using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPITests
{
    public static class Config
    {
        public static IConfiguration Init()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appSettings.json");
            IConfigurationRoot root = builder.Build();

            return root;
        }
    }
}
