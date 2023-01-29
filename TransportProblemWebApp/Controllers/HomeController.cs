using Microsoft.AspNetCore.Mvc;

namespace TransportProblemWebApp.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
