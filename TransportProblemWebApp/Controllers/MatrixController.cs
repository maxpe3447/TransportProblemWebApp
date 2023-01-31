using Microsoft.AspNetCore.Mvc;
using TransportProblemLib.Enums;
using TransportProblemWebApp.Extensions;
using TransportProblemWebApp.Model;

namespace TransportProblemWebApp.Controllers
{
    public class MatrixController : Controller
    {
        [HttpPost]
        public IActionResult AddRow(MatrixViewModel matrix)
        {

            matrix.AddRow();

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController(), matrix);
        }
        [HttpPost]
        public IActionResult AddColl(MatrixViewModel matrix)
        {
            matrix.AddColl();

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController(), matrix);
        }

        [HttpPost]
        public IActionResult RemoveRow(MatrixViewModel matrix)
        {
            matrix.RemoveRow();

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController(), matrix);
        }

        [HttpPost]
        public IActionResult RemoveColl(MatrixViewModel matrix)
        {
            matrix.RemoveColl();

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController(), matrix);
        }

        [HttpPost]
        public IActionResult GetMatrix(MatrixViewModel matrix)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController(), matrix);
            }

            return RedirectToAction(nameof(AlgorithmResultController.Index), nameof(AlgorithmResultController).CutController(), matrix);
        }
    }
}
