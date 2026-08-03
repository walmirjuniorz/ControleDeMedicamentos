using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
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

        List<ListarFuncionarioViewModel> viewModels = new List<ListarFuncionarioViewModel>();

        foreach (Funcionario f in funcionarios)
        {
            ListarFuncionarioViewModel vm = new ListarFuncionarioViewModel(f.Id, f.Nome, f.Telefone);

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
    public ActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVM)
    {
        Funcionario funcionario = new Funcionario(cadastrarVM.Nome, cadastrarVM.Telefone, cadastrarVM.Cpf);

        repositorio.Cadastrar(funcionario);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(int id)
    {
        Funcionario? funcionarioSelecionado = repositorio.SelecionarPorId(id);

        if (funcionarioSelecionado == null)
            return NotFound();

        EditarFuncionarioViewModel viewModel = new EditarFuncionarioViewModel(
            id,
            funcionarioSelecionado.Nome,
            funcionarioSelecionado.Telefone,
            funcionarioSelecionado.Cpf
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarFuncionarioViewModel editarVm)
    {
        Funcionario funcionarioAtualizado = new Funcionario(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.Cpf
        );

        bool conseguiuEditar = repositorio.Editar(editarVm.Id, funcionarioAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Funcionario? funcionarioSelecionado = repositorio.SelecionarPorId(id);

        if (funcionarioSelecionado == null)
            return NotFound();

        ExcluirFuncionarioViewModel viewModel = new ExcluirFuncionarioViewModel(id, funcionarioSelecionado.Nome);

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Excluir(ExcluirFuncionarioViewModel excluirVM)
    {
        bool ConseguiuExcluir = repositorio.Excluir(excluirVM.Id);

        if (!ConseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
