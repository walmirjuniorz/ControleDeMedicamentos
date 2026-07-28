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
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

        ViewBag.Fornecedores = fornecedores;

        return View(fornecedores);
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
    [HttpGet]
    public ActionResult Editar(int id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return NotFound();

        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

        ViewBag.Fornecedores = fornecedores;

        return View(medicamento);
    }
    [HttpPost]
    public ActionResult Editar(int id, string nome, string descricao, int fornecedorId)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(fornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento mediacamentoAtualizado = new Medicamento(nome, descricao, fornecedor);

        bool conseguiuEditar = repositorioMedicamento.Editar(id, mediacamentoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return NotFound();

        return View(medicamento);
    }
    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExcluisao(int id)
    {
        bool conseguiuExcluir = repositorioMedicamento.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
