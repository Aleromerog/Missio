using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using StringResources;

namespace MissioServer.Tests
{
    [TestFixture]
    public class HandleUserCredentialsExceptionsTests
    {
        private static HttpClient MakeClient()
        {
            return new WebApplicationFactory<Startup>().CreateClient();
        }

        [Test]
        public async Task ValidateUser_InvalidPassword_ReturnsErrorMessage()
        {
            var client = MakeClient();

            var response = await client.GetAsync("api/users/Francisco Greco/NotValid");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(AppResources.InvalidPassword, await response.Content.ReadAsAsync<string>());
        }

        [Test]
        public async Task ValidateUser_InvalidUserName_ReturnsErrorMessage()
        {
            var client = MakeClient();

            var response = await client.GetAsync("api/users/NotValidUserName/NotValid");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(AppResources.InvalidUserName, await response.Content.ReadAsAsync<string>());
        }
    }
}