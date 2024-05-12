using NetwiseApp.Services;
using System.Net;
using RichardSzalay.MockHttp;

namespace NetwiseAppTests.Services
{
	public class CatFactServiceTests
	{
		[Fact]
		public async Task CatFactServicesTestAsync()
		{
			var mockHttp = new MockHttpMessageHandler();
			var responseContent = "{\"fact\": \"Test Cat Fact\", \"length\": 13}";
			mockHttp.When("https://catfact.ninja/fact")
					.Respond(HttpStatusCode.OK, "application/json", responseContent);

			var httpClient = new HttpClient(mockHttp);
			var catFactService = new CatFactService(httpClient);
			var catFact = await catFactService.GetCatFact();
			Assert.Equal("Test Cat Fact", catFact);
		}
	}
}
