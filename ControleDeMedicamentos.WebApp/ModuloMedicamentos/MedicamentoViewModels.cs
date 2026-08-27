namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public record FornecedorMedicamentoViewModel(int Id, string Nome);

public record ListarMedicamentoViewModel(int Id, string Nome, string Descricao, string NomeFornecedor, int QuantidadeEmEstoque);

public record CadastrarMedicamentoViewModel(string Nome, string Descricao, int FornecedorId)
{
    public List<FornecedorMedicamentoViewModel> Fornecedores { get; init; } = [];
}
public record EditarMedicamentoViewModel(int Id, string Nome, string Descricao, int FornecedorId)
{
    public List<FornecedorMedicamentoViewModel> Fornecedores { get; init; } = [];
}

public record ExcluirMedicamentoViewModel(int Id, string Nome);
