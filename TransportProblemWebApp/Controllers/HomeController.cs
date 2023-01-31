using Microsoft.AspNetCore.Mvc;
using TransportProblemWebApp.Domain;

namespace TransportProblemWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }
		public IActionResult Index()
		{
			return View(_dataManager.TextField.GetTextFieldByCodeWord("PageIndex"));
		}
        public IActionResult About()
        {
            return View(_dataManager.TextField.GetTextFieldByCodeWord("PageAbout"));
        }
    }
}
