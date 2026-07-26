using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public sealed class MedicamentoController : Controller
{
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioFornecedorEmArquivo repositorioFornecedor;
    public MedicamentoController()
    {
        ContextoJson contexto = new ContextoJson();

        contexto.Carregar();

        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

    }
    [HttpGet]
    public ActionResult Listar()
    {
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        return View(medicamentos);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(string nome, string descricao, int fornecedorId)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(fornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamento = new Medicamento(nome, descricao, fornecedor);

        repositorioMedicamento.Cadastrar(medicamento);

        return RedirectToAction(nameof(Listar));
    }
}
