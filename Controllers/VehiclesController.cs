using Microsoft.AspNetCore.Mvc;

namespace React_AggressionFabrications.Controllers
{
    public class VehiclesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
