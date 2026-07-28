using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public sealed class FuncionarioController : Controller
{
    private readonly RepositorioFuncionarioEmArquivo repositorio;
    public FuncionarioController()
    {
        ContextoJson contextoJson = new ContextoJson();

        contextoJson.Carregar();

        repositorio = new RepositorioFuncionarioEmArquivo(contextoJson);
    }
    [HttpGet]
    public ActionResult Listar()
    {
        List<Funcionario> funcionarios = repositorio.SelecionarTodos();

        return View(funcionarios);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(string nome, string telefone, string cpf)
    {
        Funcionario funcionario = new Funcionario(nome, telefone, cpf);

        repositorio.Cadastrar(funcionario);

        return View(funcionario);
    }
}
