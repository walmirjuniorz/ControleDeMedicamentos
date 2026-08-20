namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public record ListarMedicamentoPrescritoRequisicaoSaidaViewModel(int Id, string Nome, int Quantidade);

public record ListarRequisicaoSaidaViewModel(int Id, string NomePaciente, DateTime Data,
    List<ListarMedicamentoPrescritoRequisicaoSaidaViewModel> MedicamentosPrescristos);

public record PacienteRequisicaoSaidaViewModel(int Id, string Nome);

public record MedicamentoPrescritoRequisicaoSaidaViewModel(
    int MedicamentoId, string NomeMedicamento, int QuantidadeEmEstoque, bool Selecionado, int Quantidade);

public record CadastrarRequisicaoSaidaViewModel(int PacienteId)
{
    public List<PacienteRequisicaoSaidaViewModel> Pacientes { get; init; } = [];
    public List<MedicamentoPrescritoRequisicaoSaidaViewModel> MedicamentosPrescritos { get; init; } = [];
}

