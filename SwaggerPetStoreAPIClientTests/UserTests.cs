using SwaggerPetStoreAPIClient.Clients;
using SwaggerPetStoreAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPIClientTests
{
    public class UserTests
    {
        public UserClient userClient { get; set; }

        public static User[] NewUsers =
        {
            new User()
            {
                id = 0,
                username = "user100",
                firstName = "user",
                lastName = "100",
                email = "user100@test.com",
                password = "password",
                phone = "12345678",
                userStatus = 0
            },
            new User()
            {
                id = 0,
                username = "user101",
                firstName = "user",
                lastName = "101",
                email = "user101@test.com",
                password = "password2",
                phone = "12345678",
                userStatus = 0
            },
            new User()
            {
                id = 0,
                username = "user102",
                firstName = "user",
                lastName = "102",
                email = "user102@test.com",
                password = "password3",
                phone = "12345678",
                userStatus = 0
            }
        };

        public static User[][] NewUsersInArray = { NewUsers };
        
        public static List<User>[] NewUsersInList = new List<User>[] { NewUsers.ToList() };

        public static string[] NullOrEmptyString = { "", null };

        public static User[] UsersToUpdate =
        {
            new User()
            {
                id = 0,
                username = "user100",
                firstName = "user",
                lastName = "100",
                email = "user100@test.com",
                password = "password",
                phone = "12345678",
                userStatus = 0
            }
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            userClient = new UserClient();
        }

        #region CreateUser

        [TestCaseSource(nameof(NewUsers)), Order(1)]
        public void CreateUser_Should_CreateUser_When_UserIsValid(User newUser)
        {
            User createdUser = userClient.CreateUser(newUser);

            Assert.IsNotNull(createdUser);
            Assert.That(createdUser.code, Is.EqualTo(200));
        }

        [Test, Order(2)]
        public void CreateUser_Should_ReturnError_When_UserIsNull()
        {
            User createdUser = userClient.CreateUser(null);
            Assert.IsNotNull(createdUser);
            Assert.That(createdUser.code, Is.EqualTo(405));
        }

        #endregion

        #region CreateUsersWithArray

        [TestCaseSource(nameof(NewUsersInArray)), Order(3)]
        public void CreateUsersWithArray_Should_CreateUsers_When_UsersArrayIsValid(User[] newUsers)
        {
            ApiResponse createdUsersResponse = userClient.CreateUsersWithArray(newUsers);

            Assert.IsNotNull(createdUsersResponse);
            Assert.That(createdUsersResponse.code, Is.EqualTo(200));
        }

        [Test, Order(4)]
        public void CreateUsersWithArray_Should_ReturnError_When_UsersArrayIsEmpty()
        {
            User[] newUsers = new User[0];
            ApiResponse createdUsersResponse = userClient.CreateUsersWithArray(newUsers);
            Assert.IsNotNull(createdUsersResponse);
            Assert.That(createdUsersResponse.code, Is.EqualTo(405));
        }

        [Test, Order(5)]
        public void CreateUsersWithArray_Should_ReturnError_When_UsersArrayIsNull()
        {
            ApiResponse createdUsersResponse = userClient.CreateUsersWithArray(null);
            Assert.IsNotNull(createdUsersResponse);
            Assert.That(createdUsersResponse.code, Is.EqualTo(405));
        }

        #endregion

        #region CreateUsersWithList

        [TestCaseSource(nameof(NewUsersInList)), Order(6)]
        public void CreateUsersWithList_Should_CreateUsers_When_UsersListIsValid(List<User> newUsers)
        {
            ApiResponse createdUsersResponse = userClient.CreateUsersWithList(newUsers);

            Assert.IsNotNull(createdUsersResponse);
            Assert.That(createdUsersResponse.code, Is.EqualTo(200));
        }

        [Test, Order(7)]
        public void CreateUsersWithList_Should_ReturnError_When_UsersListIsEmpty()
        {
            List<User> newUsers = new List<User>();
            ApiResponse createdUsersResponse = userClient.CreateUsersWithList(newUsers);
            Assert.IsNotNull(createdUsersResponse);
            Assert.That(createdUsersResponse.code, Is.EqualTo(405));
        }

        [Test, Order(8)]
        public void CreateUsersWithList_Should_ReturnError_When_UsersListIsNull()
        {
            ApiResponse createdUsersResponse = userClient.CreateUsersWithList(null);
            Assert.IsNotNull(createdUsersResponse);
            Assert.That(createdUsersResponse.code, Is.EqualTo(405));
        }

        #endregion

        #region GetUserByUsername

        [TestCase("user100"), Order(9)]
        public void GetUserByUsername_Should_GetUser_When_UsernameIsValid(string username)
        {
            User user = userClient.GetUserByUsername(username);
            Assert.IsNotNull(user);
            Assert.That(user.username, Is.EqualTo(username));
            Assert.That(user.code, Is.EqualTo(0));
        }

        [TestCase("-1"), Order(10)]
        public void GetUserByUsername_Should_ReturnError_When_UsernameIsInvalid(string username)
        {
            User user = userClient.GetUserByUsername(username);
            Assert.IsNotNull(user);
            Assert.That(user.code, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(NullOrEmptyString)), Order(11)]
        public void GetUserByUsername_Should_ReturnError_When_UsernameIsNullOrEmpty(string username)
        {
            User user = userClient.GetUserByUsername(username);
            Assert.IsNotNull(user);
            Assert.That(user.code, Is.EqualTo(405));
        }

        #endregion

        #region Login

        [TestCase("user100", "password"), Order(12)]
        public void Login_Should_LoginUser_When_InputIsValid(string username, string password)
        {
            User loggedInUser = userClient.Login(username, password);
            Assert.IsNotNull(loggedInUser);
            Assert.That(loggedInUser.code, Is.EqualTo(200));
        }

        [TestCase("user100", ""), Order(13)]
        [TestCase("", "password")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void Login_Should_ReturnError_When_UsernameOrPasswordAreNullOrEmpty(string username, string password)
        {
            User loggedInUser = userClient.Login(username, password);
            Assert.IsNotNull(loggedInUser);
            Assert.That(loggedInUser.code, Is.EqualTo(405));
        }

        #endregion

        #region Logout

        [Test, Order(14)]
        public void Logout_Should_LogoutUser()
        {
            User loggedOutUser = userClient.Logout();
            Assert.IsNotNull(loggedOutUser);
            Assert.That(loggedOutUser.code, Is.EqualTo(200));
        }

        #endregion

        #region UpdateUser

        [TestCaseSource(nameof(UsersToUpdate)), Order(15)]
        public void UpdateUser_Should_UpdateUser_When_InputIsValid(User userToUpdate)
        {
            string username = userToUpdate.username;
            User updatedUser = userClient.UpdateUser(username, userToUpdate);

            Assert.IsNotNull(updatedUser);
            Assert.That(updatedUser.code, Is.EqualTo(200));
        }

        [TestCaseSource(nameof(NullOrEmptyString)), Order(16)]
        public void UpdateUser_Should_ReturnError_When_UsernameIsNullOrEmpty(string username)
        {
            User updatedUser = userClient.UpdateUser(username, UsersToUpdate[0]);

            Assert.IsNotNull(updatedUser);
            Assert.That(updatedUser.code, Is.EqualTo(405));
        }

        [Test, Order(17)]
        public void UpdateUser_Should_ReturnError_When_UserIsNull()
        {
            string username = "user100";
            User updatedUser = userClient.UpdateUser(username, null);

            Assert.IsNotNull(updatedUser);
            Assert.That(updatedUser.code, Is.EqualTo(405));
        }

        [Test, Order(18)]
        public void UpdateUser_Should_ReturnError_When_UserAndUsernameAreNull()
        {
            User updatedUser = userClient.UpdateUser(null, null);
            Assert.IsNotNull(updatedUser);
            Assert.That(updatedUser.code, Is.EqualTo(405));
        }

        #endregion

        #region DeleteUser

        [TestCase("user100"), Order(19)]
        public void DeleteUser_Should_DeleteUser_When_UsernameIsValid(string username)
        {
            User deletedUser = userClient.DeleteUser(username);
            Assert.IsNotNull(deletedUser);
            Assert.That(deletedUser.code, Is.EqualTo(200));
        }

        [TestCaseSource(nameof(NullOrEmptyString)), Order(20)]
        public void DeleteUser_Should_ReturnError_When_UsernameIsNullOrEmpty(string username)
        {
            User deletedUser = userClient.DeleteUser(username);
            Assert.IsNotNull(deletedUser);
            Assert.That(deletedUser.code, Is.EqualTo(405));
        }

        #endregion

    }
}
