using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Threading.Tasks;
using System.Net;
using FluentAssertions;

namespace WeatherApp.Api.IntegrationTests
{
    // https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api#override-injected-instances-of-the-di-container
    public class WeatherForecastControllerTests
    {
        [Fact]
        public async Task GET_retrieves_weather_forecast()
        {
            await using var application = new WebApplicationFactory<Program>();
            
            using var client = application.CreateClient();

            var response = await client.GetAsync("/weatherforecast");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}