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
    public class SuppliersTests
    {
        Mock<DbSet<SUPPLIER>> mockSupplierSet = new Mock<DbSet<SUPPLIER>>();
        Mock<PODbEntities> mockContext = new Mock<PODbEntities>();
        IQueryable<SUPPLIER> supplierList;
        SuppliersController supplierController;

        [SetUp]
        public void Initialize()
        {
            //Arrange
            supplierList = new List<SUPPLIER>
            {
                new SUPPLIER{SUPLNO="1", SUPLNAME="samsung", SUPLADDR="South Korea"},
                new SUPPLIER{SUPLNO="2", SUPLNAME="apple", SUPLADDR="US"},
                new SUPPLIER{SUPLNO="3", SUPLNAME="tesla", SUPLADDR="US"},
                new SUPPLIER{SUPLNO="4", SUPLNAME="mistibushi", SUPLADDR="japan"},
                new SUPPLIER{SUPLNO="5", SUPLNAME="lenova", SUPLADDR="US"}
            }.AsQueryable();

            mockSupplierSet.As<IQueryable<SUPPLIER>>().Setup(m => m.Provider).Returns(supplierList.Provider);
            mockSupplierSet.As<IQueryable<SUPPLIER>>().Setup(m => m.Expression).Returns(supplierList.Expression);
            mockSupplierSet.As<IQueryable<SUPPLIER>>().Setup(m => m.ElementType).Returns(supplierList.ElementType);
            mockSupplierSet.As<IQueryable<SUPPLIER>>().Setup(m => m.GetEnumerator()).Returns(supplierList.GetEnumerator());
            mockSupplierSet.Setup(a => a.Find(It.IsAny<object[]>())).Returns<object[]>(b => supplierList.FirstOrDefault(c => c.SUPLNO == b[0].ToString()));
            mockContext.Setup(a => a.SUPPLIERs).Returns(mockSupplierSet.Object);
            mockContext.Setup(a => a.MarkAsModified(It.IsAny<SUPPLIER>()));

            supplierController = new SuppliersController(mockContext.Object);
        }

        [Test]
        public void Suppliers_GetSUPPLIERs_ShouldReturnAllSuppliers()
        {
            //Arrange
            //Initialize();

            //Act
            List<SUPPLIER> result = supplierController.GetSUPPLIERs().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void Suppliers_GetSUPPLIER_ShouldReturnsSupplierById()
        {
            //Arrange
            //Initialize();

            //Act
            OkNegotiatedContentResult<SUPPLIER> response = supplierController.GetSUPPLIER("1") as OkNegotiatedContentResult<SUPPLIER>;
            SUPPLIER supplier = response.Content;

            //Assert
            Assert.IsNotNull(supplier);
            Assert.AreEqual("1", supplier.SUPLNO);
            Assert.AreEqual("samsung", supplier.SUPLNAME);
            Assert.AreEqual("South Korea", supplier.SUPLADDR);
        }

        [Test]
        public void Suppliers_PutSUPPLIER_ShouldReturnStatusCode()
        {
            //Arrange
            //Initialize();
            SUPPLIER supplier = new SUPPLIER { SUPLNO = "1", SUPLNAME = "Hp", SUPLADDR = "South Korea" };

            //Act
            StatusCodeResult result = supplierController.PutSUPPLIER("1", supplier) as StatusCodeResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void Suppliers_PostSUPPLIER_ShouldReturnSameSupplier()
        {
            //Arrange
            //Initialize();
            SUPPLIER supplier = new SUPPLIER { SUPLNO = "6", SUPLNAME = "Hp", SUPLADDR = "US" };

            //Act
            CreatedAtRouteNegotiatedContentResult<SUPPLIER> result = supplierController.PostSUPPLIER(supplier) as CreatedAtRouteNegotiatedContentResult<SUPPLIER>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.SUPLNO);
            Assert.AreEqual(result.Content.SUPLNAME, supplier.SUPLNAME);
        }

        [Test]
        public void Suppliers_DeleteSupplier_ShouldReturnOK()
        {
            //Arrange
            //Initialize();

            //Act
            OkNegotiatedContentResult<SUPPLIER> result = supplierController.DeleteSUPPLIER("1") as OkNegotiatedContentResult<SUPPLIER>;

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Content.SUPLNO);
        }
    }
}
