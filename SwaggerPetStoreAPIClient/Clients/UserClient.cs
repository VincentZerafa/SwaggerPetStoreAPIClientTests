using Newtonsoft.Json;
using SwaggerPetStoreAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPIClient.Clients
{
    public class UserClient : APIClient
    {
        public UserClient() : base()
        {
        }

        public User GetUserByUsername(string username)
        {
            string result = CallGetAPI($"user/{username}").Result;
            User user = JsonConvert.DeserializeObject<User>(result);
            return user;
        }

        public User CreateUser(User user)
        {
            string result = CallPostAPI("user", user).Result;
            User createdUser = JsonConvert.DeserializeObject<User>(result);
            return createdUser;
        }

        public ApiResponse CreateUsersWithArray(User[] users)
        {
            string result = CallPostAPI($"user/createWithArray", users).Result;
            ApiResponse createdUsersResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
            return createdUsersResponse;
        }

        public ApiResponse CreateUsersWithList(List<User> users)
        {
            string result = CallPostAPI($"user/createWithArray", users).Result;
            ApiResponse createdUsersResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
            return createdUsersResponse;
        }

        public User UpdateUser(string username, User user)
        {
            string result = CallPutAPI($"user/{username}", user).Result;
            User createdUser = JsonConvert.DeserializeObject<User>(result);
            return createdUser;
        }

        public User DeleteUser(string username)
        {
            string result = CallDeleteAPI($"user/{username}").Result;
            User deletedUser = JsonConvert.DeserializeObject<User>(result);
            return deletedUser;
        }

        public User Login(string username, string password)
        {
            string result = CallGetAPI($"user/login?username={username}&password={password}").Result;
            User deletedUser = JsonConvert.DeserializeObject<User>(result);
            return deletedUser;
        }

        public User Logout()
        {
            string result = CallGetAPI($"user/logout").Result;
            User deletedUser = JsonConvert.DeserializeObject<User>(result);
            return deletedUser;
        }
    }
}
