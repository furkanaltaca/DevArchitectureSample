using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Tests.Helpers;
using Tests.Helpers.TokenHelpers;

namespace Tests.Services.Authentication
{
    [TestFixture]
    public class TokenTest : BaseIntegrationTest
    {
        [Test]
        public async Task TokenAthorizeTest()
        {
            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("api/users/getall");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [Test]
        public async Task TokenExpiredTest()
        {
            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Thread.Sleep(10000);

            var response = await _client.GetAsync("api/users/getall");
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);

        }

    }
}
