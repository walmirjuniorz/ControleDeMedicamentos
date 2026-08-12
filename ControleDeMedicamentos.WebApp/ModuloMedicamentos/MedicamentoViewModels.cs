using ControleDeMedicamentos.WebApp.ModuloFornecedores;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public record ListarMedicamentoViewModel(int Id, string Nome, string Descricao, Fornecedor Fornecedor, string QuantidadeEmEstoque);

public record CadastrarMedicamentoViewModel(int Id, string Nome, string Descricao, Fornecedor Fornecedor, string QuantidadeEmEstoque);

public record EditarMedicamentoViewModel(int Id, string Nome, string Descricao, Fornecedor Fornecedor, string QuantidadeEmEstoque);

public record ExcluirMedicamentoViewModel(int Id, string Nome);
