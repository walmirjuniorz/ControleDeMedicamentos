using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public sealed class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorio;
    public PacienteController()
    {
        ContextoJson contextoJson = new ContextoJson();

        contextoJson.Carregar();

        repositorio = new RepositorioPacienteEmArquivo(contextoJson);
    }
    [HttpGet]
    public ActionResult Listar()
    {
        List<Paciente> pacientes = repositorio.SelecionarTodos();

        List<ListarPacienteViewModel> viewModels = new List<ListarPacienteViewModel>();

        foreach (Paciente p in pacientes)
        {
            ListarPacienteViewModel vm = new ListarPacienteViewModel(p.Id, p.Nome, p.Telefone);

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
    public ActionResult Cadastrar(CadastrarPacienteViewModel cadastroVm)
    {
        Paciente paciente = new Paciente(cadastroVm.Nome, cadastroVm.Telefone, cadastroVm.CartaoSus, cadastroVm.Cpf);

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
    public ActionResult Editar(EditarPacienteViewModel editarVm)
    {
        Paciente pacienteAtualizado = new Paciente(editarVm.Nome, editarVm.Telefone, editarVm.CartaoSus, editarVm.Cpf);

        bool conseguiuEditar = repositorio.Editar(editarVm.Id, pacienteAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Paciente? pacienteSelecionado = repositorio.SelecionarPorId(id);

        if (pacienteSelecionado == null)
            return NotFound();

        ExcluirPacienteViewModel viewModels = new ExcluirPacienteViewModel(id, pacienteSelecionado.Nome);

        return View(pacienteSelecionado);
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
