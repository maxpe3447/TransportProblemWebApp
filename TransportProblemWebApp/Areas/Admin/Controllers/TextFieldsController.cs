using Microsoft.AspNetCore.Mvc;
using TransportProblemWebApp.Domain;
using TransportProblemWebApp.Domain.Entities;
using TransportProblemWebApp.Extensions;

namespace TransportProblemWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TextFieldsController : Controller
    {
        private readonly DataManager _dataManager;

        public TextFieldsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public IActionResult Edit(string codeWord)
        {
            var entity = _dataManager.TextField.GetTextFieldByCodeWord(codeWord);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(TextField model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _dataManager.TextField.SaveTextField(model);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}
