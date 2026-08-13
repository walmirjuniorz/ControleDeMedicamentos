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

        List<ListarMedicamentoViewModel> viewModels = [];

        foreach (Medicamento med in medicamentos)
        {
            ListarMedicamentoViewModel viewModel = new ListarMedicamentoViewModel(
                med.Id, med.Nome, med.Descricao, med.Fornecedor.Nome, med.QuantidadeEmEstoque
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMedicamentoViewModel viewModel = new CadastrarMedicamentoViewModel(string.Empty, string.Empty, 0)
        with
        { Fornecedores = ObterFornecedores() };

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(cadastrarVm.FornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamento = new Medicamento(cadastrarVm.Nome, cadastrarVm.Descricao, fornecedor);

        repositorioMedicamento.Cadastrar(medicamento);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(int id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return NotFound();

        EditarMedicamentoViewModel viewModel = new EditarMedicamentoViewModel(
            id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.Fornecedor.Id
        )
        with
        { Fornecedores = ObterFornecedores() };

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel editarVm)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(editarVm.FornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento mediacamentoAtualizado = new Medicamento(editarVm.Nome, editarVm.Descricao, fornecedor);

        bool conseguiuEditar = repositorioMedicamento.Editar(editarVm.Id, mediacamentoAtualizado);

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

        ExcluirMedicamentoViewModel viewModel = new ExcluirMedicamentoViewModel(id, medicamento.Nome);

        return View(viewModel);
    }
    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExcluisao(ExcluirMedicamentoViewModel excluirVm)
    {
        bool conseguiuExcluir = repositorioMedicamento.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
    private List<FornecedorMedicamentoViewModel> ObterFornecedores()
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

        List<FornecedorMedicamentoViewModel> fornecedoresVms = [];

        foreach (Fornecedor f in fornecedores)
        {
            FornecedorMedicamentoViewModel fornecedorVm = new FornecedorMedicamentoViewModel(f.Id, f.Nome);

            fornecedoresVms.Add(fornecedorVm);
        }
        return fornecedoresVms;
    }
}
