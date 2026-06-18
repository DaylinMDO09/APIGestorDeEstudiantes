using Microsoft.AspNetCore.Mvc;

namespace APIestudiantes.Controllers
{
    public class EstudiantesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
