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

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(int id)
    {
        Funcionario? funcionario = repositorio.SelecionarPorId(id);

        if (funcionario == null)
            return NotFound();

        return View(funcionario);
    }
    [HttpPost]
    public ActionResult Editar(int id, string nome, string telefone, string cpf)
    {
        Funcionario funcionarioAtualizado = new Funcionario(nome, telefone, cpf);

        bool conseguiuEditar = repositorio.Editar(id, funcionarioAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Funcionario? funcionario = repositorio.SelecionarPorId(id);

        if (funcionario == null)
            return NotFound();

        return View(funcionario);
    }
    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(int id)
    {
        bool ConfirmarExclusao = repositorio.Excluir(id);

        if (!ConfirmarExclusao)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
