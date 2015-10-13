using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAPIDemo.Controllers;
using WebAPIDemo.Models;

namespace WebAPIDemo.Tests.Controllers
{
    [TestClass]
    public class DepartmentsControllerTest
    {
        private Mock<IDepartmentRepository> depRepoMock;
        private DepartmentsController controller;

        [TestInitialize]
        public void Setup()
        {
            depRepoMock = new Mock<IDepartmentRepository>();
            controller = new DepartmentsController(depRepoMock.Object);

            controller.Request = new HttpRequestMessage { RequestUri = new Uri("http://server.com/api/category") };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                "DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        [TestMethod]
        public void WhenGetAllIsCalled_AllDepartmentsAreReturned()
        {
            //Arrange
            var listOfDepartments = new List<Department>
            {
                new Department { Name="Test Department", Id = 1, LandLineNumber = "888-8888" },
                new Department { Name="Grin Department", Id = 2, LandLineNumber = "999-9999" }
            };
            depRepoMock
                .Setup(c => c.GetAllDepartments())
                .Returns(listOfDepartments);

            //Act
            var actionResult = controller.GetDepartments();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Department>>;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<Department>>));
            Assert.AreEqual(contentResult.Content.Count(), listOfDepartments.Count);
            
        }
    }
    
}
