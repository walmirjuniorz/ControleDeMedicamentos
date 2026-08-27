using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoSaidaController : Controller
{
    private readonly RepositorioRequisicaoSaidaEmArquivo repositorio;
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;

    public RequisicaoSaidaController(
        RepositorioRequisicaoSaidaEmArquivo repositorio,
        RepositorioPacienteEmArquivo repositorioPaciente,
        RepositorioMedicamentoEmArquivo repositorioMedicamento)
    {
        this.repositorio = repositorio;
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioMedicamento = repositorioMedicamento;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoSaidaViewModel> viewModels = [];

        foreach (RequisicaoSaida requisicao in repositorio.SelecionarTodos())
        {
            List<ListarMedicamentoPrescritoRequisicaoSaidaViewModel> medicamentosPrescritosViewModels = [];

            foreach (MedicamentoPrescrito prescrito in requisicao.MedicamentosPrescritos)
            {
                ListarMedicamentoPrescritoRequisicaoSaidaViewModel prescritoVm = new(
                    prescrito.Medicamento.Id,
                    prescrito.Medicamento.Nome,
                    prescrito.Quantidade
                );

                medicamentosPrescritosViewModels.Add(prescritoVm);
            }

            ListarRequisicaoSaidaViewModel viewModel = new ListarRequisicaoSaidaViewModel(
                requisicao.Id,
                requisicao.Paciente.Nome,
                requisicao.Data,
                medicamentosPrescritosViewModels
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarRequisicaoSaidaViewModel viewModel = new CadastrarRequisicaoSaidaViewModel(0)
        with
        { Pacientes = ObterPacientes(), MedicamentosPrescritos = ObterMedicamentos() };

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarRequisicaoSaidaViewModel viewModel)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(viewModel.PacienteId);

        if (paciente == null)
            return NotFound();

        List<MedicamentoPrescritoRequisicaoSaidaViewModel> medicamentosModel = viewModel.MedicamentosPrescritos ?? [];

        List<MedicamentoPrescrito> medicamentosPrescritos = [];

        foreach (MedicamentoPrescritoRequisicaoSaidaViewModel medicamentoModel in medicamentosModel)
        {
            if (!medicamentoModel.Selecionado)
                continue;

            Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(medicamentoModel.MedicamentoId);
            medicamentosPrescritos.Add(new MedicamentoPrescrito(medicamento!, medicamentoModel.Quantidade));
        }

        RequisicaoSaida requisicao = new RequisicaoSaida(paciente, medicamentosPrescritos);

        repositorio.Cadastrar(requisicao);

        return RedirectToAction(nameof(Listar));
    }

    private List<PacienteRequisicaoSaidaViewModel> ObterPacientes()
    {
        List<PacienteRequisicaoSaidaViewModel> viewModels = [];

        foreach (Paciente paciente in repositorioPaciente.SelecionarTodos())
        {
            PacienteRequisicaoSaidaViewModel viewModel = new PacienteRequisicaoSaidaViewModel(paciente.Id, paciente.Nome);

            viewModels.Add(viewModel);
        }

        return viewModels;
    }

    private List<MedicamentoPrescritoRequisicaoSaidaViewModel> ObterMedicamentos(
        List<MedicamentoPrescritoRequisicaoSaidaViewModel>? valoresEnviados = null
    )
    {
        Dictionary<int, MedicamentoPrescritoRequisicaoSaidaViewModel> valoresPorMedicamento = [];

        if (valoresEnviados != null)
        {
            foreach (MedicamentoPrescritoRequisicaoSaidaViewModel valor in valoresEnviados)
                valoresPorMedicamento[valor.MedicamentoId] = valor;
        }

        List<MedicamentoPrescritoRequisicaoSaidaViewModel> viewModels = [];

        foreach (Medicamento medicamento in repositorioMedicamento.SelecionarTodos())
        {
            valoresPorMedicamento.TryGetValue(medicamento.Id, out MedicamentoPrescritoRequisicaoSaidaViewModel? valor);

            viewModels.Add(new MedicamentoPrescritoRequisicaoSaidaViewModel(
                medicamento.Id, medicamento.Nome, medicamento.QuantidadeEmEstoque,
                valor?.Selecionado ?? false, valor?.Quantidade ?? 0
            ));
        }
        return viewModels;
    }
}
