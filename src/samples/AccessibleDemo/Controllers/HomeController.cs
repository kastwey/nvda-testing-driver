using AccessibleDemo.Models;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AccessibleDemo.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<IActionResult> TreeViewExample()
		{
			var viewModel = JsonConvert.DeserializeObject<TreeViewViewModel>(
				System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "wwwroot", "jsonFiles", "wcag21.json")));

			return await Task.FromResult(View(viewModel));
		}

	}
}
