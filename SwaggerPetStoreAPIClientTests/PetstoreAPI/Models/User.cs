using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPITests.PetstoreAPI.Models
{
    public class User : ApiResponse, IEqualityComparer<User>
    {
        public long id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public int userStatus { get; set; }

        public bool Equals(User x, User y)
        {
            if (x == null || y == null)
                return false;

            return x.id == y.id
                && x.username == y.username
                && x.firstName == y.firstName
                && x.lastName == y.lastName
                && x.email == y.email
                && x.password == y.password
                && x.phone == y.phone
                && x.userStatus == y.userStatus;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }
}
