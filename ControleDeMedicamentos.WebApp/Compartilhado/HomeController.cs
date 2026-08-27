using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Compartilhado;

public class HomeController : Controller
{
    // Açoes = Métodos
    public ActionResult Index()
    {
        return View();
    }
}
