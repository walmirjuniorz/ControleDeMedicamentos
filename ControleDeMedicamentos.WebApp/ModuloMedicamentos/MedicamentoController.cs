using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public sealed class MedicamentoController : Controller
{
    private readonly RepositorioMedicamentoEmArquivo repositorio;
    public MedicamentoController()
    {
        ContextoJson contexto = new ContextoJson();

        contexto.Carregar();

        repositorio = new RepositorioMedicamentoEmArquivo(contexto);
    }
    [HttpGet]
    public ActionResult Listar()
    {
        List<Medicamento> medicamentos = repositorio.SelecionarTodos();

        return View(medicamentos);
    }
}
