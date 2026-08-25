using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoEntradaController : Controller
{
    private readonly RepositorioRequisicaoEntradaEmArquivo repositorio;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;

    public RequisicaoEntradaController(
        RepositorioRequisicaoEntradaEmArquivo repositorio,
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioFuncionarioEmArquivo repositorioFuncionario)
    {
        this.repositorio = repositorio;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoEntradaViewModel> viewModels = [];

        foreach (RequisicaoEntrada requisicao in repositorio.SelecionarTodos())
        {
            if (requisicao.Medicamento is null || requisicao.Funcionario is null)
                continue;

            ListarRequisicaoEntradaViewModel viewModel = new ListarRequisicaoEntradaViewModel(
                requisicao.Id,
                requisicao.Medicamento.Nome,
                requisicao.Funcionario.Nome,
                requisicao.Quantidade,
                requisicao.Data
            );

            viewModels.Add(viewModel);
        }
        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarRequisicaoEntradaViewModel viewModel = new CadastrarRequisicaoEntradaViewModel(0, 0, 0)
        with
        {
            Medicamentos = ObterMedicamentos(),
            Funcionarios = ObterFuncionarios()
        };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarRequisicaoEntradaViewModel cadastrarVm)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(cadastrarVm.MedicamentoId);
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(cadastrarVm.FuncionarioId);

        if (medicamento == null || funcionario == null)
            return NotFound();

        RequisicaoEntrada requisicao = new RequisicaoEntrada(
            medicamento,
            cadastrarVm.Quantidade,
            funcionario
        );

        repositorio.Cadastrar(requisicao);

        return RedirectToAction(nameof(Listar));
    }

    private List<MedicamentoRequisicaoEntradaViewModel> ObterMedicamentos()
    {
        List<MedicamentoRequisicaoEntradaViewModel> viewModels = [];

        foreach (Medicamento medicamento in repositorioMedicamento.SelecionarTodos())
            viewModels.Add(new MedicamentoRequisicaoEntradaViewModel(medicamento.Id, medicamento.Nome));

        return viewModels;
    }

    private List<FuncionarioRequisicaoEntradaViewModel> ObterFuncionarios()
    {
        List<FuncionarioRequisicaoEntradaViewModel> viewModels = [];

        foreach (Funcionario funcionario in repositorioFuncionario.SelecionarTodos())
            viewModels.Add(new FuncionarioRequisicaoEntradaViewModel(funcionario.Id, funcionario.Nome));

        return viewModels;
    }
}
