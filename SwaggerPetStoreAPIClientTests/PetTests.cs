using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SwaggerPetStoreAPIClient.Models;
using System.Xml.Linq;
using SwaggerPetStoreAPIClient.Clients;
using System.Net;

namespace SwaggerPetStoreAPIClientTests
{
    public class PetTests
    {
        public PetClient petClient { get; set; }

        public static Pet[] NewPets =
        {
            new Pet()
            {
                id = 1,
                category = new Category()
                {
                    id = 0,
                    name = "animal"
                },
                name = "rabbit",
                photoUrls = new List<string>()
                {
                    "string"
                },
                tags = new List<Tag>()
                {
                    new Tag() { id = 0, name = "rabbit" }
                },
                status = "available"
            },
            new Pet()
            {
                id = 100,
                category = new Category()
                {
                    id = 0,
                    name = "animal"
                },
                name = "cat",
                photoUrls = new List<string>()
                {
                    "string"
                },
                tags = new List<Tag>()
                {
                    new Tag() { id = 0, name = "cat" }
                },
                status = "available"
            },
            new Pet()
            {
                id = long.MaxValue,
                category = new Category()
                {
                    id = 0,
                    name = "animal"
                },
                name = "dog",
                photoUrls = new List<string>()
                {
                    "string"
                },
                tags = new List<Tag>()
                {
                    new Tag() { id = 0, name = "dog" }
                },
                status = "available"
            }
        };

        public static Pet[] InvalidPets =
        {
            new Pet()
            {
                id = 0,
                category = new Category(),
                name = null,
                photoUrls = new List<string>(),
                tags = new List<Tag>(),
                status = null
            },
            new Pet()
            {
                id = -5,
                category = new Category(),
                name = null,
                photoUrls = new List<string>(),
                tags = new List<Tag>(),
                status = null
            }
        };

        public static long[] NewPetIDs = NewPets.Select(x => x.id).ToArray();

        public static Pet[] PetsToUpdate =
        {
            new Pet()
            {
                id = NewPetIDs[1],
                category = new Category()
                {
                    id = 0,
                    name = "animal"
                },
                name = "doggie",
                photoUrls = new List<string>()
                {
                    "string"
                },
                tags = new List<Tag>()
                {
                    new Tag() { id = 0, name = "dog" }
                },
                status = "sold"
            }
        };

        public static long[] InvalidPetIDs = InvalidPets.Select(x => x.id).ToArray();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            petClient = new PetClient();
        }

        #region CreatePet

        [TestCaseSource(nameof(NewPets)), Order(1)]
        public void CreatePet_Should_AddPet_When_PetIsValid(Pet newPet)
        {
            Pet addedPet = petClient.CreatePet(newPet);

            Assert.IsNotNull(addedPet);
            Assert.That(addedPet.Equals(newPet, addedPet), Is.True);
            Assert.That(addedPet.code, Is.EqualTo(0));
        }

        [TestCaseSource(nameof(InvalidPets)), Order(2)]
        public void CreatePet_Should_ReturnError_When_PetIsInvalid(Pet newPet)
        {
            Pet addedPet = petClient.CreatePet(newPet);

            Assert.IsNotNull(addedPet);
            Assert.That(addedPet.code, Is.EqualTo(405));
        }

        [Test, Order(3)]
        public void CreatePet_Should_ReturnError_When_PetIsNull()
        {
            Pet addedPet = petClient.CreatePet(null);
            Assert.IsNotNull(addedPet);
            Assert.That(addedPet.code, Is.EqualTo(405));
        }

        #endregion

        #region GetPetByID

        [TestCaseSource(nameof(NewPetIDs)), Order(4)]
        public void GetPetByID_Should_ReturnPetByID_When_IDIsPositive(long petID)
        {
            Pet pet = petClient.GetPetByID(petID);
            Assert.IsNotNull(pet);
            Assert.That(pet.id, Is.EqualTo(petID));
            Assert.That(pet.code, Is.EqualTo(0));
        }

        [TestCaseSource(nameof(InvalidPetIDs)), Order(5)]
        public void GetPetByID_Should_ReturnError_When_IDIsZeroOrNegative(long petID)
        {
            Pet pet = petClient.GetPetByID(petID);
            Assert.IsNotNull(pet);
            Assert.That(pet.code, Is.EqualTo(1));
        }

        #endregion

        #region GetPetsByStatus

        [TestCase("available"), Order(6)]
        [TestCase("pending")]
        [TestCase("sold")]
        public void GetPetsByStatus_Should_ReturnPetsWithMatchedStatus_When_StatusIsValid(string status)
        {
            List<Pet> pets = petClient.GetPetsByStatus(status);

            Assert.IsNotNull(pets);

            foreach (Pet pet in pets)
                Assert.That(pet.status.Equals(status), Is.True);
        }

        [TestCase("-1"), Order(7)]
        public void GetPetsByStatus_Should_ReturnNone_When_StatusIsInvalid(string status)
        {
            List<Pet> pets = petClient.GetPetsByStatus(status);

            Assert.IsNotNull(pets);
            Assert.IsEmpty(pets);
        }

        #endregion

        #region UpdatePetByID

        [TestCase(100, "my pet", "sold"), Order(8)]
        [TestCase(100, "", "sold")]
        [TestCase(100, null, "sold")]
        [TestCase(100, "my pet", "")]
        [TestCase(100, "my pet", null)]
        public void UpdatePetByID_Should_UpdatePet_When_InputIsValid(long petID, string name, string status)
        {
            Pet petUpdateResponse = petClient.UpdatePetByID(petID, name, status);
            Pet petAfterUpdate = petClient.GetPetByID(petID);

            Assert.IsNotNull(petUpdateResponse);
            Assert.That(petUpdateResponse.code, Is.EqualTo(200));

            Assert.IsNotNull(petAfterUpdate);
            Assert.That(petAfterUpdate.id, Is.EqualTo(petID));
            Assert.That(petAfterUpdate.name, Is.EqualTo(name));
            Assert.That(petAfterUpdate.status, Is.EqualTo(status));
            Assert.That(petAfterUpdate.code, Is.EqualTo(0));
        }

        [TestCase(0, "my pet", "sold"), Order(9)]
        [TestCase(-10, "my pet", "sold")]
        public void UpdatePetByID_Should_ReturnError_When_IDIsInvalid(long petID, string name, string status)
        {
            Pet petUpdateResponse = petClient.UpdatePetByID(petID, name, status);

            Assert.IsNotNull(petUpdateResponse);
            Assert.That(petUpdateResponse.code, Is.EqualTo(404));
        }

        #endregion

        #region UpdatePet

        [TestCaseSource(nameof(PetsToUpdate)), Order(10)]
        public void UpdatePet_Should_UpdatePet_When_PetIsValid(Pet petToUpdate)
        {
            Pet updatedPet = petClient.UpdatePet(petToUpdate);

            Assert.IsNotNull(updatedPet);
            Assert.That(updatedPet.Equals(petToUpdate, updatedPet), Is.True);
            Assert.That(updatedPet.code, Is.EqualTo(0));
        }

        [Test, Order(11)]
        public void UpdatePet_Should_ReturnError_When_PetIsNull()
        {
            Pet updatedPet = petClient.UpdatePet(null);
            Assert.IsNotNull(updatedPet);
            Assert.That(updatedPet.code, Is.EqualTo(405));
        }

        #endregion

        #region UploadPetImage

        [Test, Order(12)]
        public void UploadPetImage_Should_UploadImage_When_InputIsValid()
        {
            long petID = 1;
            string additionalMetadata = "test";
            byte[] file = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestImages\", "dog-cartoon.avif"));

            Pet updatedPet = petClient.UploadPetImage(petID, additionalMetadata, file);

            Assert.IsNotNull(updatedPet);
            Assert.That(updatedPet.code, Is.EqualTo(200));
        }

        [Test, Order(13)]
        public void UploadPetImage_Should_ReturnError_When_ImageIsEmpty()
        {
            long petID = 1;
            string additionalMetadata = "test";
            byte[] file = new byte[0];

            Pet updatedPet = petClient.UploadPetImage(petID, additionalMetadata, file);

            Assert.IsNotNull(updatedPet);
            Assert.That(updatedPet.code, Is.EqualTo(400));
        }

        #endregion

        #region DeletePetByID

        [TestCaseSource(nameof(NewPetIDs)), Order(14)]
        public void DeletePetByID_Should_DeletePet_When_IDIsValid(long petID)
        {
            Pet petUpdateResponse = petClient.DeletePetByID(petID);
            Pet petAfterDelete = petClient.GetPetByID(petID);

            Assert.IsNotNull(petUpdateResponse);
            Assert.That(petUpdateResponse.code, Is.EqualTo(200));

            Assert.IsNotNull(petAfterDelete);
            Assert.That(petAfterDelete.code, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(InvalidPetIDs)), Order(15)]
        public void DeletePetByID_Should_ReturnError_When_IDIsInvalid(long petID)
        {
            Pet petUpdateResponse = petClient.DeletePetByID(petID);

            Assert.IsNotNull(petUpdateResponse);
            Assert.That(petUpdateResponse.code, Is.EqualTo(404));
        }

        #endregion
    }
}