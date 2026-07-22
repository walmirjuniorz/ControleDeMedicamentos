using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

public class HomeController : Controller
{
    // Açoes = Métodos
    public ActionResult Index()
    {
        return View();
    }
}
