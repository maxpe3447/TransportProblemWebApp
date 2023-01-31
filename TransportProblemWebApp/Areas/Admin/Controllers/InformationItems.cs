using Microsoft.AspNetCore.Mvc;
using TransportProblemWebApp.Domain;
using TransportProblemWebApp.Domain.Entities;
using TransportProblemWebApp.Extensions;

namespace TransportProblemWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InformationItems : Controller
    {
        private readonly DataManager _dataManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public InformationItems(DataManager dataManager, 
                                IWebHostEnvironment hostEnvironment)
        {
            _dataManager = dataManager;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Edit(Guid id)
        {
            var entity = id == default
                ? new InformationField()
                : _dataManager.InformationField.GetInformationFieldById(id);

            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(InformationField entity, IFormFile? titleImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }

            if (titleImageFile != null)
            {
                entity.TitleImagePath = titleImageFile.FileName;
                using (var stream = new FileStream(Path.Combine(_hostEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                {
                    titleImageFile.CopyTo(stream);
                }
            }
            _dataManager.InformationField.SaveInformationField(entity);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}
