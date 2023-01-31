using Microsoft.AspNetCore.Mvc;
using TransportProblemWebApp.Domain;
using TransportProblemWebApp.Model;

namespace TransportProblemWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }
		public IActionResult Index(MatrixViewModel matrix = null)
        {
            ViewBag.TextField = _dataManager.TextField.GetTextFieldByCodeWord("PageIndex");

            return View(matrix == null ? new MatrixViewModel() : matrix);
		}
        public IActionResult About()
        {
            return View(_dataManager.TextField.GetTextFieldByCodeWord("PageAbout"));
        }
    }
}
