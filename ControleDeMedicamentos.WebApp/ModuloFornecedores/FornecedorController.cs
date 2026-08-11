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
    public ActionResult Cadastrar(CadastrarFornecedorViewModel cadastroVM)
    {
        Fornecedor fornecedor = new Fornecedor(cadastroVM.Nome, cadastroVM.Telefone, cadastroVM.Cnpj);

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
    public ActionResult Editar(EditarFornecedorViewModel editarVM)
    {
        Fornecedor fornecedorAtualizado = new Fornecedor(editarVM.Nome, editarVM.Telefone, editarVM.Cnpj);

        bool conseguiuEditar = repositorio.Editar(editarVM.Id, fornecedorAtualizado);

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
    public ActionResult ConfirmarExclusao(ExcluirFornecedorViewModel excluirVM)
    {
        bool conseguiuExcluir = repositorio.Excluir(excluirVM.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
