using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwaggerPetStoreAPITests.PetstoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwaggerPetStoreAPITests.PetstoreAPI.Clients
{
    public class PetClient : APIClient
    {
        public PetClient() : base()
        {
        }

        public Pet GetPetByID(long petID)
        {
            string result = CallGetAPI($"pet/{petID}").Result;
            Pet pet = JsonConvert.DeserializeObject<Pet>(result);
            return pet;
        }

        public Pet UpdatePetByID(long petID, string name, string status)
        {
            List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", name),
                new KeyValuePair<string, string>("status", status)
            };

            string result = CallPostAPIWithFormData($"pet/{petID}", formData).Result;
            Pet pet = JsonConvert.DeserializeObject<Pet>(result);
            return pet;
        }

        public Pet DeletePetByID(long petID)
        {
            string result = CallDeleteAPI($"pet/{petID}").Result;
            Pet pet = JsonConvert.DeserializeObject<Pet>(result);
            return pet;
        }

        public List<Pet> GetPetsByStatus(string status)
        {
            string result = CallGetAPI($"pet/findByStatus?status={status}").Result;
            List<Pet> pets = JsonConvert.DeserializeObject<List<Pet>>(result);
            return pets;
        }

        public Pet CreatePet(Pet pet)
        {
            string result = CallPostAPI("pet/", pet).Result;
            Pet addedPet = JsonConvert.DeserializeObject<Pet>(result);
            return addedPet;
        }

        public Pet UpdatePet(Pet pet)
        {
            string result = CallPutAPI("pet/", pet).Result;
            Pet updatedPet = JsonConvert.DeserializeObject<Pet>(result);
            return updatedPet;
        }

        public Pet UploadPetImage(long petID, string additionalMetadata, byte[] file)
        {
            string result = CallPostAPIWithFile($"pet/{petID}/uploadImage", additionalMetadata, file).Result;
            Pet pet = JsonConvert.DeserializeObject<Pet>(result);
            return pet;
        }
    }
}
