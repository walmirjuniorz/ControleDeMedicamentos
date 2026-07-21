using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
