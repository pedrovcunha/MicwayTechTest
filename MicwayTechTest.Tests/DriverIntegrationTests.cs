using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MicwayTechTest.Tests
{
    [TestClass]
    public class DriverIntegrationTests
    {
        private readonly HttpClient _client;

        public DriverIntegrationTests()
        {
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public void DriverGetAllTest()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Drivers/");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        [DataRow(1)]
        public void DriverGetOneTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Drivers/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void DriverPostTest()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"/api/Drivers/");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void DriverPutTest(int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"/api/Drivers/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [TestMethod]
        [DataRow(10)]
        public void DriversDeleteTest(int id)
        {
            // Arrange
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("DELETE"), $"/api/Drivers/{id}");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
