using Newtonsoft.Json;
using SwaggerPetStoreAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPIClient.Clients
{
    public class StoreClient : APIClient
    {
        public StoreClient() : base()
        {
        }

        public Order GetOrderByID(long orderID)
        {
            string result = CallGetAPI($"store/order/{orderID}").Result;
            Order pet = JsonConvert.DeserializeObject<Order>(result);
            return pet;
        }

        public Order DeleteOrderByID(long orderID)
        {
            string result = CallDeleteAPI($"store/order/{orderID}").Result;
            Order pet = JsonConvert.DeserializeObject<Order>(result);
            return pet;
        }

        public Dictionary<string, int> GetStatusCodeQuantities()
        {
            string result = CallGetAPI("store/inventory").Result;
            Dictionary<string, int> statusCodeQuantities = JsonConvert.DeserializeObject<Dictionary<string, int>>(result);
            return statusCodeQuantities;
        }

        public Order PlaceOrderForPet(Order order)
        {
            string result = CallPostAPI("store/order", order).Result;
            Order placedOrder = JsonConvert.DeserializeObject<Order>(result);
            return placedOrder;
        }
    }
}
