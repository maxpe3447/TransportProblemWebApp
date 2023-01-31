using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using TransportProblemLib.Enums;
using TransportProblemWebApp.Model;
using TransportProblemWebApp.Service.MinElementAlgorithm;

namespace TransportProblemWebApp.Controllers
{
    public class AlgorithmResultController : Controller
    {
        private readonly ITransportAlgorithm _transportAlgorithm;

        public AlgorithmResultController(ITransportAlgorithm transport)
        {
            _transportAlgorithm = transport;
        }

        public IActionResult Index(MatrixViewModel model)
        {
            var result = _transportAlgorithm.GetResultModel(model);

            return View(result);
        }
    }
}
