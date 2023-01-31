using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using TransportProblemWebApp.Domain;

namespace TransportProblemWebApp.Controllers
{
    public class InformationController : Controller
    {
        private readonly DataManager _dataManager;

        public InformationController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public IActionResult Index(Guid id)
        {
            ViewBag.TextField = _dataManager.TextField.GetTextFieldByCodeWord("PageInformations");
            if (id != default)
            {
                return View("ShowInformations", _dataManager.InformationField.GetInformationFieldById(id));
            }

            return View(_dataManager.InformationField.GetInformationFields());
        }
    }
}
