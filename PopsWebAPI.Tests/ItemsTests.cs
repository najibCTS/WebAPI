using Moq;
using NUnit.Framework;
using PopsWebAPI.Controllers;
using PopsWebAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http.Results;

namespace PopsWebAPI.Tests
{
    [TestFixture]
    public class ItemsTests
    {
        Mock<DbSet<ITEM>> mockItemSet = new Mock<DbSet<ITEM>>();
        Mock<PODbEntities> mockContext = new Mock<PODbEntities>();
        IQueryable<ITEM> itemList;
        ItemsController itemController;

        [SetUp]
        public void Initialize()
        {
            //Arrange
            itemList = new List<ITEM>
            {
                new ITEM{ ITCODE = "1", ITDESC="TV", ITRATE=5000},
                new ITEM{ ITCODE = "2", ITDESC="Shoe", ITRATE=300},
                new ITEM{ ITCODE = "3", ITDESC="Car", ITRATE=23000},
                new ITEM{ ITCODE = "4", ITDESC="Mobile", ITRATE=1000},
                new ITEM{ ITCODE = "5", ITDESC="Laptop", ITRATE=3000}
            }.AsQueryable();

            mockItemSet.As<IQueryable<ITEM>>().Setup(m => m.Provider).Returns(itemList.Provider);
            mockItemSet.As<IQueryable<ITEM>>().Setup(m => m.Expression).Returns(itemList.Expression);
            mockItemSet.As<IQueryable<ITEM>>().Setup(m => m.ElementType).Returns(itemList.ElementType);
            mockItemSet.As<IQueryable<ITEM>>().Setup(m => m.GetEnumerator()).Returns(itemList.GetEnumerator());
            mockItemSet.Setup(a => a.Find(It.IsAny<object[]>())).Returns<object[]>(b => itemList.FirstOrDefault(c => c.ITCODE == b[0].ToString()));
            mockContext.Setup(a => a.ITEMs).Returns(mockItemSet.Object);
            mockContext.Setup(a => a.MarkAsModified(It.IsAny<ITEM>()));

            itemController = new ItemsController(mockContext.Object);
        }

        [Test]
        public void Items_GetItems_ShouldReturnAllItems()
        {
            //Arrange
            //Initialize();

            //Act
            List<ITEM> result = itemController.GetITEMs().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void Items_GetItem_ShouldReturnItemById()
        {
            //Arrange
            //Initialize();

            //Act
            OkNegotiatedContentResult<ITEM> response = itemController.GetITEM("1") as OkNegotiatedContentResult<ITEM>;
            ITEM item = response.Content;

            //Assert
            Assert.IsNotNull(item);
            Assert.AreEqual("1", item.ITCODE);
            Assert.AreEqual("TV", item.ITDESC);
            Assert.AreEqual(5000, item.ITRATE);
        }

        [Test]
        public void Items_PutItem_ShouldReturnStatusCode()
        {
            //Arrange
            //Initialize();
            ITEM item = new ITEM { ITCODE = "1", ITDESC = "Chair", ITRATE = 5000 };

            //Act
            StatusCodeResult result = itemController.PutITEM("1", item) as StatusCodeResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void Items_PostItem_ShouldReturnSameItem()
        {
            //Arrange
            //Initialize();
            ITEM item = new ITEM { ITCODE = "6", ITDESC = "Chair", ITRATE = 200 };

            //Act
            CreatedAtRouteNegotiatedContentResult<ITEM> result = itemController.PostITEM(item) as CreatedAtRouteNegotiatedContentResult<ITEM>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.ITCODE);
            Assert.AreEqual(result.Content.ITCODE, item.ITCODE);
        }

        [Test]
        public void Items_DeleteItem_ShouldReturnOK()
        {
            //Arrange
            //Initialize();

            //Act
            OkNegotiatedContentResult<ITEM> result = itemController.DeleteITEM("1") as OkNegotiatedContentResult<ITEM>;

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Content.ITCODE);
        }
    }
}
