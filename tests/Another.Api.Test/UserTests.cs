using Another.Api.Test.Models;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Another.Api.Test
{
    public class UserTests : IClassFixture<WebApplicationFactory<StartupTests>>
    {
        private readonly WebApplicationFactory<StartupTests> _factory;
        public HttpClient Client;

        private const string BASE_URL = "https://localhost:5001/api/v1/Account";

        public UserTests(WebApplicationFactory<StartupTests> factory)
        {
            _factory = factory;
            Client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Create User")]
        [Trait("Integration", "User")]
        public async Task User_Register_ShouldRegisterSuccessfully()
        {
            //Arrange
            var faker = new Faker("pt_BR");
            var userEmail = faker.Internet.Email().ToLower();
            var userPass = faker.Internet.Password(7, false, "", "A1@");

            var userToRegister = new
            {
                Email = userEmail,
                Password = userPass,
                ConfirmPassword = userPass
            };

            string json = JsonConvert.SerializeObject(userToRegister);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"{BASE_URL}/sign-up")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            //Act
            var response = await Client.SendAsync(postRequest);

            // Assert 
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseString);
            Assert.True(user.Success);
            Assert.NotNull(user.Data);
            Assert.Null(user.Errors);
            Assert.NotEmpty(user.Data.AccessToken);
        }

        [Fact(DisplayName = "Do not register User")]
        [Trait("Integration", "User")]
        public async Task User_Register_ShouldNotRegister()
        {
            //Arrange
            var userToRegister = new
            {
                Email = "minhaconta2@eumail.com",
                Password = "Ab@c4te",
                ConfirmPassword = "Ab@c4te"
            };

            string json = JsonConvert.SerializeObject(userToRegister);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"{BASE_URL}/sign-up")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            //Act
            var response = await Client.SendAsync(postRequest);

            // Assert 
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseString);
            Assert.False(user.Success);
            Assert.NotNull(user.Errors);
            Assert.Null(user.Data);
        }

        [Fact(DisplayName = "Login User")]
        [Trait("Integration", "User")]
        public async Task User_Login_ShouldLogin()
        {
            //Arrange
            var userToRegister = new
            {
                Email = "minhaconta@eumail.com",
                Password = "Ab@c4te",
            };

            string json = JsonConvert.SerializeObject(userToRegister);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"{BASE_URL}/sign-in")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            //Act
            var response = await Client.SendAsync(postRequest);

            // Assert 
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseString);
            Assert.True(user.Success);
            Assert.NotNull(user.Data);
            Assert.NotEmpty(user.Data.AccessToken);
            Assert.Null(user.Errors);
        }

        [Fact(DisplayName = "User can not login")]
        [Trait("Integration", "User")]
        public async Task User_Login_ShouldNotLogin()
        {
            //Arrange
            var faker = new Faker("pt_BR");
            var userEmail = faker.Internet.Email().ToLower();
            var userPass = faker.Internet.Password(7, false, "", "A1@");

            var userToRegister = new
            {
                Email = userEmail,
                Password = userPass,
                ConfirmPassword = userPass
            };

            string json = JsonConvert.SerializeObject(userToRegister);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"{BASE_URL}/sign-in")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            //Act
            var response = await Client.SendAsync(postRequest);

            // Assert 
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseString);
            
            Assert.False(user.Success);
            Assert.NotNull(user.Errors);
            Assert.Null(user.Data);
        }

    }
}
