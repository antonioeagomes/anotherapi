using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Another.Api.Test
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<StartupTests>>
    {
        private readonly WebApplicationFactory<StartupTests> _factory;

        public UnitTest1(WebApplicationFactory<StartupTests> factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "Get")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("https://localhost:5001/api/v1/Suppliers");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            
        }
    }
}
