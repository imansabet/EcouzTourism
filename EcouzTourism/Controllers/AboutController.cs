using Microsoft.AspNetCore.Mvc;

namespace EcouzTourism.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
