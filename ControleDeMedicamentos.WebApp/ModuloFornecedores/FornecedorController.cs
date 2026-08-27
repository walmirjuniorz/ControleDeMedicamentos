using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores;

public sealed class FornecedorController : Controller
{
    private readonly RepositorioFornecedorEmArquivo repositorio;

    public FornecedorController(RepositorioFornecedorEmArquivo repositorio)
    {
        this.repositorio = repositorio;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Fornecedor> fornecedores = repositorio.SelecionarTodos();

        List<ListarFornecedorViewModel> viewModels = new List<ListarFornecedorViewModel>();

        foreach (Fornecedor f in fornecedores)
        {
            ListarFornecedorViewModel vm = new ListarFornecedorViewModel(f.Id, f.Nome, f.Telefone, f.Cnpj);

            viewModels.Add(vm);
        }
        return View(viewModels);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVm)
    {
        Fornecedor fornecedor = new Fornecedor(
            cadastrarVm.Nome ?? string.Empty,
            cadastrarVm.Telefone ?? string.Empty,
            cadastrarVm.Cnpj ?? string.Empty);

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        repositorio.Cadastrar(fornecedor);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(int id)
    {
        Fornecedor? fornecedorSelecionado = repositorio.SelecionarPorId(id);

        if (fornecedorSelecionado == null)
            return NotFound();

        EditarFornecedorViewModel viewModel = new EditarFornecedorViewModel(
            id,
            fornecedorSelecionado.Nome,
            fornecedorSelecionado.Telefone,
            fornecedorSelecionado.Cnpj
        );

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Editar(EditarFornecedorViewModel editarVm)
    {
        Fornecedor fornecedorAtualizado = new Fornecedor(editarVm.Nome, editarVm.Telefone, editarVm.Cnpj);

        if (!ModelState.IsValid)
            return View(editarVm);

        bool conseguiuEditar = repositorio.Editar(editarVm.Id, fornecedorAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Fornecedor? fornecedorSelecionado = repositorio.SelecionarPorId(id);

        if (fornecedorSelecionado == null)
            return NotFound();

        ExcluirFornecedorViewModel viewModel = new ExcluirFornecedorViewModel(id, fornecedorSelecionado.Nome);

        return View(viewModel);
    }
    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(ExcluirFornecedorViewModel excluirVm)
    {
        bool conseguiuExcluir = repositorio.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
