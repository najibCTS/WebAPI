using Moq;
using NUnit.Framework;
using PopsWebAPI.Controllers;
using PopsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http.Results;

namespace PopsWebAPI.Tests
{
    [TestFixture]
    public class PomastersTests
    {
        Mock<DbSet<POMASTER>> mockPomasterSet = new Mock<DbSet<POMASTER>>();
        Mock<PODbEntities> mockContext = new Mock<PODbEntities>();
        IQueryable<POMASTER> pomasterList;
        PomastersController pomasterController;

        [SetUp]
        public void Initialize()
        {
            //Arrange
            pomasterList = new List<POMASTER>
            {
                new POMASTER{ PONO="1", PODATE=DateTime.Now, SUPLNO="1"},
                new POMASTER{ PONO="2", PODATE=DateTime.Now, SUPLNO="1"},
                new POMASTER{ PONO="3", PODATE=DateTime.Now, SUPLNO="2"},
                new POMASTER{ PONO="4", PODATE=DateTime.Now, SUPLNO="2"},
                new POMASTER{ PONO="5", PODATE=DateTime.Now, SUPLNO="3"}
            }.AsQueryable();

            mockPomasterSet.As<IQueryable<POMASTER>>().Setup(m => m.Provider).Returns(pomasterList.Provider);
            mockPomasterSet.As<IQueryable<POMASTER>>().Setup(m => m.Expression).Returns(pomasterList.Expression);
            mockPomasterSet.As<IQueryable<POMASTER>>().Setup(m => m.ElementType).Returns(pomasterList.ElementType);
            mockPomasterSet.As<IQueryable<POMASTER>>().Setup(m => m.GetEnumerator()).Returns(pomasterList.GetEnumerator());
            mockPomasterSet.Setup(a => a.Find(It.IsAny<object[]>())).Returns<object[]>(b => pomasterList.FirstOrDefault(c => c.PONO == b[0].ToString()));
            mockContext.Setup(a => a.POMASTERs).Returns(mockPomasterSet.Object);
            mockContext.Setup(a => a.MarkAsModified(It.IsAny<POMASTER>()));

            pomasterController = new PomastersController(mockContext.Object);
        }

        [Test]
        public void Pomasters_GetPomasters_ShouldReturnAllPomasters()
        {
            //Arrange
            //Initialize();

            //Act
            List<POMASTER> result = pomasterController.GetPOMASTERs().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void Pomasters_GetPomaster_ShouldReturnPomasterById()
        {
            //Arrange
            //Initialize();

            //Act
            OkNegotiatedContentResult<POMASTER> response = pomasterController.GetPOMASTER("1") as OkNegotiatedContentResult<POMASTER>;
            POMASTER pomaster = response.Content;

            //Assert
            Assert.IsNotNull(pomaster);
            Assert.AreEqual("1", pomaster.PONO);
            Assert.AreEqual(DateTime.Now.Date, pomaster.PODATE.Value.Date);
            Assert.AreEqual("1", pomaster.SUPLNO);
        }

        [Test]
        public void Pomasters_PutPomaster_ShouldReturnStatusCode()
        {
            //Arrange
            //Initialize();
            POMASTER pOMASTER = new POMASTER { PONO = "1", PODATE = DateTime.Now, SUPLNO = "4" };

            //Act
            StatusCodeResult result = pomasterController.PutPOMASTER("1", pOMASTER) as StatusCodeResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void Pomasters_PostPomaster_ShouldReturnSamePomaster()
        {
            //Arrange
            //Initialize();
            POMASTER pOMASTER = new POMASTER { PONO = "6", PODATE = DateTime.Now, SUPLNO = "4" };

            //Act
            CreatedAtRouteNegotiatedContentResult<POMASTER> result = pomasterController.PostPOMASTER(pOMASTER) as CreatedAtRouteNegotiatedContentResult<POMASTER>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.PONO);
            Assert.AreEqual(result.Content.SUPLNO, pOMASTER.SUPLNO);
        }

        [Test]
        public void Pomasters_DeletePomaster_ShouldReturnOK()
        {
            //Arrange
            //Initialize();

            //Act
            OkNegotiatedContentResult<POMASTER> result = pomasterController.DeletePOMASTER("1") as OkNegotiatedContentResult<POMASTER>;

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Content.PONO);
        }
    }
}
