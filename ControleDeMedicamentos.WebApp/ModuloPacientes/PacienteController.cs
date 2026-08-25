using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public sealed class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorio;

    public PacienteController(RepositorioPacienteEmArquivo repositorio)
    {
        this.repositorio = repositorio;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarPacienteViewModel> viewModels = [];

        foreach (Paciente p in repositorio.SelecionarTodos())
        {
            ListarPacienteViewModel viewModel = new ListarPacienteViewModel(p.Id, p.Nome, p.Telefone, p.CartaoSus);

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarPacienteViewModel viewModel = new CadastrarPacienteViewModel(string.Empty, string.Empty, string.Empty, string.Empty);

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarPacienteViewModel viewModel)
    {
        Paciente paciente = new Paciente(viewModel.Nome, viewModel.Telefone, viewModel.CartaoSus, viewModel.Cpf);

        repositorio.Cadastrar(paciente);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(int id)
    {
        Paciente? pacienteSelecionado = repositorio.SelecionarPorId(id);

        if (pacienteSelecionado == null)
            return NotFound();

        EditarPacienteViewModel viewModel = new EditarPacienteViewModel(
            id,
            pacienteSelecionado.Nome,
            pacienteSelecionado.Telefone,
            pacienteSelecionado.CartaoSus,
            pacienteSelecionado.Cpf
        );

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Editar(EditarPacienteViewModel viewModel)
    {
        Paciente pacienteAtualizado = new Paciente(viewModel.Nome, viewModel.Telefone, viewModel.CartaoSus, viewModel.Cpf);

        bool conseguiuEditar = repositorio.Editar(viewModel.Id, pacienteAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Paciente? paciente = repositorio.SelecionarPorId(id);

        if (paciente == null)
            return NotFound();

        ExcluirPacienteViewModel viewModel = new ExcluirPacienteViewModel(id, paciente.Nome);

        return View(viewModel);
    }
    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(ExcluirPacienteViewModel excluirVm)
    {
        bool conseguiuExcluir = repositorio.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
