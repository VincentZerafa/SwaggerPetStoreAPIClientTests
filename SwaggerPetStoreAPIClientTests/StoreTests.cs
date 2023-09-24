using SwaggerPetStoreAPIClient.Clients;
using SwaggerPetStoreAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPIClientTests
{
    public class StoreTests
    {
        public StoreClient storeClient { get; set; }

        public static Order[] NewOrders =
        {
            new Order()
            {
                id = 1,
                petid = 1,
                quantity= 1,
                shipDate= DateTime.Now,
                status= "placed",
                complete= true
            },
            new Order()
            {
                id = 5,
                petid = 1,
                quantity= 1,
                shipDate= DateTime.Now,
                status= "placed",
                complete= true
            },
            new Order()
            {
                id = 10,
                petid = 1,
                quantity= 1,
                shipDate= DateTime.Now,
                status= "placed",
                complete= true
            }
        };

        public static long[] NewOrderIDs = NewOrders.Select(x => x.id).ToArray();

        public static Order[] InvalidOrders =
        {
            new Order()
            {
                id = -1,
                petid = -1,
                quantity= -1,
                shipDate= DateTime.Now,
                status= null,
                complete= true
            }
        };

        public static long[] InvalidOrderIDs = { -10, 0, 11, 20 };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            storeClient = new StoreClient();
        }

        #region CreateOrderForPet

        [TestCaseSource(nameof(NewOrders)), Order(1)]
        public void CreateOrderForPet_Should_CreateOrder_When_OrderIsValid(Order newOrder)
        {
            Order placedOrder = storeClient.PlaceOrderForPet(newOrder);
            Assert.IsNotNull(placedOrder);
            Assert.That(placedOrder.Equals(newOrder, placedOrder));
            Assert.That(placedOrder.code, Is.EqualTo(0));
        }

        [TestCaseSource(nameof(InvalidOrders)), Order(2)]
        public void CreateOrderForPet_Should_ReturnError_When_OrderIsInvalid(Order newOrder)
        {
            Order placedOrder = storeClient.PlaceOrderForPet(newOrder);
            Assert.IsNotNull(placedOrder);
            Assert.That(placedOrder.code, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void CreateOrderForPet_Should_ReturnError_When_OrderIsNull()
        {
            Order placedOrder = storeClient.PlaceOrderForPet(null);
            Assert.IsNotNull(placedOrder);
            Assert.That(placedOrder.code, Is.EqualTo(1));
        }

        #endregion

        #region GetOrderByID

        [TestCaseSource(nameof(NewOrderIDs)), Order(4)]
        public void GetOrderByID_Should_ReturnOrderByID_When_IDIsValid(long orderID)
        {
            Order order = storeClient.GetOrderByID(orderID);
            Assert.IsNotNull(order);
            Assert.That(order.id, Is.EqualTo(orderID));
            Assert.That(order.code, Is.EqualTo(0));
        }

        [TestCaseSource(nameof(InvalidOrderIDs)), Order(5)]
        public void GetOrderByID_Should_ReturnError_When_IDIsInvalid(long orderID)
        {
            Order order = storeClient.GetOrderByID(orderID);
            Assert.IsNotNull(order);
            Assert.That(order.code, Is.EqualTo(1));
        }

        #endregion

        #region GetStatusCodeQuantities

        [Test, Order(6)]
        public void GetStatusCodeQuantities_Should_GetStatusCodeQuantities()
        {
            Dictionary<string, int> statusCodeQuantities = storeClient.GetStatusCodeQuantities();
            Assert.IsNotNull(statusCodeQuantities);
            Assert.IsNotEmpty(statusCodeQuantities);
        }

        #endregion

        #region DeleteOrderByID

        [TestCaseSource(nameof(NewOrderIDs)), Order(7)]
        public void DeleteOrderByID_Should_DeleteOrderByID_When_IDIsValid(long orderID)
        {
            Order order = storeClient.DeleteOrderByID(orderID);
            Order orderAfterDelete = storeClient.GetOrderByID(orderID);

            Assert.IsNotNull(order);
            Assert.That(order.code, Is.EqualTo(200));

            Assert.IsNotNull(orderAfterDelete);
            Assert.That(orderAfterDelete.code, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(InvalidOrderIDs)), Order(8)]
        public void DeleteOrderByID_Should_ReturnError_When_IDIsInvalid(long orderID)
        {
            Order order = storeClient.DeleteOrderByID(orderID);
            Assert.IsNotNull(order);
            Assert.That(order.code, Is.EqualTo(404));
        }

        #endregion

    }
}
