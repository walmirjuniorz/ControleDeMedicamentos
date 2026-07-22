using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores;

public sealed class FornecedorController : Controller
{
    private readonly RepositorioFornecedorEmArquivo repositorio;
    public FornecedorController()
    {
        ContextoJson contextoJson = new ContextoJson();

        contextoJson.Carregar();

        repositorio = new RepositorioFornecedorEmArquivo(contextoJson);
    }
    [HttpGet]
    public ActionResult Listar()
    {
        List<Fornecedor> fornecedores = repositorio.SelecionarTodos();

        return View(fornecedores);
    }
}
