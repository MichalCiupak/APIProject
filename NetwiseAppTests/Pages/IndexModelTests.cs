using NetwiseApp.Pages;
using System.Net;
using RichardSzalay.MockHttp;
using NetwiseApp.Services;

namespace NetwiseAppTests.Pages
{
	public class IndexModelTests
	{
		[Fact]
		public async Task OnPostAsync_SavesCatFactToFile()
		{

			var mockHttp = new MockHttpMessageHandler();
			var responseContent = "{\"fact\": \"Test Cat Fact\", \"length\": 13}";
			mockHttp.When("https://catfact.ninja/fact")
					.Respond(HttpStatusCode.OK, "application/json", responseContent);

			var httpClient = new HttpClient(mockHttp);
			var catFactService = new CatFactService(httpClient);

			var pageModel = new IndexModel(catFactService);

			await pageModel.OnPostAsync();

			var filePath = "cat_facts.txt";
			Assert.True(File.Exists(filePath));

			string[] lines = await File.ReadAllLinesAsync(filePath);
			Assert.NotEmpty(lines);

			string lastLine = lines[^1];
			var expectedLine = $"{DateTime.Now}: Test Cat Fact";
			Assert.Equal(expectedLine, lastLine);
		}
	}
}
