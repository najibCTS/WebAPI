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
    public class PodetailsTests
    {
        Mock<DbSet<PODETAIL>> mockPodetailSet = new Mock<DbSet<PODETAIL>>();
        Mock<PODbEntities> mockContext = new Mock<PODbEntities>();
        IQueryable<PODETAIL> podetailList;
        PodetailsController podetailController;

        [SetUp]
        public void Initialize()
        {
            //Arrange
            podetailList = new List<PODETAIL>
            {
                new PODETAIL{ PONO="1", ITCODE="1", QTY=2},
                new PODETAIL{ PONO="2", ITCODE="1", QTY=132},
                new PODETAIL{ PONO="3", ITCODE="3", QTY=345},
                new PODETAIL{ PONO="4", ITCODE="3", QTY=4},
                new PODETAIL{ PONO="5", ITCODE="4", QTY=12}
            }.AsQueryable();

            mockPodetailSet.As<IQueryable<PODETAIL>>().Setup(m => m.Provider).Returns(podetailList.Provider);
            mockPodetailSet.As<IQueryable<PODETAIL>>().Setup(m => m.Expression).Returns(podetailList.Expression);
            mockPodetailSet.As<IQueryable<PODETAIL>>().Setup(m => m.ElementType).Returns(podetailList.ElementType);
            mockPodetailSet.As<IQueryable<PODETAIL>>().Setup(m => m.GetEnumerator()).Returns(podetailList.GetEnumerator());
            mockPodetailSet.Setup(a => a.Find(It.IsAny<object[]>())).Returns<object[]>(b => podetailList.FirstOrDefault(c => c.PONO == b[0].ToString()));
            mockContext.Setup(a => a.PODETAILs).Returns(mockPodetailSet.Object);
            mockContext.Setup(a => a.MarkAsModified(It.IsAny<PODETAIL>()));

            podetailController = new PodetailsController(mockContext.Object);
        }

        [Test]
        public void Podetails_GetPodetails_ShouldReturnAllPodetails()
        {
            //Arrange
            //Initialize();

            //Act
            List<PODETAIL> result = podetailController.GetPODETAILs().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void Podetails_GetPodetail_ShouldReturnPodetailById()
        {
            //Arrange
            //Initialize();

            //Act
            OkNegotiatedContentResult<PODETAIL> response = podetailController.GetPODETAIL("1", "1") as OkNegotiatedContentResult<PODETAIL>;
            PODETAIL podetail = response.Content;

            //Assert
            Assert.IsNotNull(podetail);
            Assert.AreEqual("1", podetail.PONO);
            Assert.AreEqual("1", podetail.ITCODE);
            Assert.AreEqual(2, podetail.QTY);
        }

        [Test]
        public void Podetails_PutPodetail_ShouldReturnStatusCode()
        {
            //Arrange
            //Initialize();
            PODETAIL pODETAIL = new PODETAIL{ PONO = "1", ITCODE = "1", QTY = 100};

            //Act
            StatusCodeResult result = podetailController.PutPODETAIL("1", "1", pODETAIL) as StatusCodeResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void Podetails_PostPodetail_ShouldReturnSamePodetail()
        {
            //Arrange
            //Initialize();
            PODETAIL pODETAIL = new PODETAIL { PONO = "6", ITCODE = "2", QTY = 543 };

            //Act
            CreatedAtRouteNegotiatedContentResult<PODETAIL> result = podetailController.PostPODETAIL(pODETAIL) as CreatedAtRouteNegotiatedContentResult<PODETAIL>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.PONO);
            Assert.AreEqual(result.Content.ITCODE, pODETAIL.ITCODE);
        }

        [Test]
        public void Podetails_DeletePodetail_ShouldReturnOK()
        {
            //Arrange
            //Initialize();

            //Act
            OkNegotiatedContentResult<PODETAIL> result = podetailController.DeletePODETAIL("1", "1") as OkNegotiatedContentResult<PODETAIL>;

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Content.PONO);
        }
    }
}
