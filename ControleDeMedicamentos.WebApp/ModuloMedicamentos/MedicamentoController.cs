using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public sealed class MedicamentoController : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        return View();
    }
}
