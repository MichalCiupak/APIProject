using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetwiseApp.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetwiseApp.Pages
{
	public class IndexModel : PageModel
	{
		private readonly CatFactService _catFactService;

		public IndexModel(CatFactService catFactService)
		{
			_catFactService = catFactService;
		}

		[BindProperty]
		public string CatFactMessage { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			string? catFact = await _catFactService.GetCatFact();
			if (!string.IsNullOrEmpty(catFact))
			{
				string filePath = "cat_facts.txt";
				string newLine = $"{DateTime.Now}: {catFact}";

				using (StreamWriter writer = System.IO.File.AppendText(filePath))
				{
					await writer.WriteLineAsync(newLine);
				}

				CatFactMessage = catFact;
			}

			return Page();
		}
	}
}
