using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public sealed class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorio;
    public PacienteController()
    {
        ContextoJson contextoJson = new ContextoJson();

        contextoJson.Carregar();

        repositorio = new RepositorioPacienteEmArquivo(contextoJson);
    }
    [HttpGet]
    public ActionResult Listar()
    {
        List<Paciente> pacientes = repositorio.SelecionarTodos();

        return View(pacientes);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(string nome, string telefone, string cartaoSus, string cpf)
    {
        Paciente paciente = new Paciente(nome, telefone, cartaoSus, cpf);

        repositorio.Cadastrar(paciente);

        return RedirectToAction(nameof(Listar));
    }
}
