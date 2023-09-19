using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPITests.PetstoreAPI.Models
{
    public class ApiResponse
    {
        public int code { get; set; }
        public string type { get; set; }
        public string message { get; set; }
    }
}
