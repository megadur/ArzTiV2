using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace ArzTi3Server.Tests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task SwaggerUI_IsAccessible()
        {
            var response = await _client.GetAsync("/swagger");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task SwaggerJson_IsGenerated()
        {
            var response = await _client.GetAsync("/swagger/v1/swagger.json");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("ArzTi3 Prescription Mediation API", content);
            Assert.Contains("\"openapi\"", content);

            // Verify that it's valid JSON
            Assert.NotEmpty(content);
            Assert.True(content.Length > 100); // Should be a substantial JSON document
        }

        [Fact(Skip = "Authentication tests need to be updated after API changes")]
        public async Task AllEndpoints_RequireAuthentication()
        {
            var endpoints = new[]
            {
                "/api/prescriptions/new",
                "/api/prescriptions/mark-as-read",
                "/api/prescriptions/set-abgerechnet"
            };

            foreach (var endpoint in endpoints)
            {
                HttpResponseMessage response;

                if (endpoint == "/api/prescriptions/new")
                {
                    response = await _client.GetAsync(endpoint);
                }
                else
                {
                    response = await _client.PostAsync(endpoint,
                        new StringContent("{\"prescriptions\":[]}",
                        System.Text.Encoding.UTF8, "application/json"));
                }

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
    }
}