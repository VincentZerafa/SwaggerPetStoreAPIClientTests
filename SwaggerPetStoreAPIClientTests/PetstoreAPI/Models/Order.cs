using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPITests.PetstoreAPI.Models
{
    public class Order : ApiResponse, IEqualityComparer<Order>
    {
        public long id { get; set; }
        public long petid { get; set; }
        public int quantity { get; set; }
        public DateTime shipDate { get; set; }
        public string status { get; set; }
        public bool complete { get; set; }

        public bool Equals(Order x, Order y)
        {
            if (x == null || y == null)
                return false;

            return x.id == y.id
                && x.petid == y.petid
                && x.quantity == y.quantity
                && x.shipDate.Date == y.shipDate.Date
                && x.status == y.status
                && x.complete == y.complete;
        }

        public int GetHashCode([DisallowNull] Order obj)
        {
            return obj.GetHashCode();
        }
    }
}
