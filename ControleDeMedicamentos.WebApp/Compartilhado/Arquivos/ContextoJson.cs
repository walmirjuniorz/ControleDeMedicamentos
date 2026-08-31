using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;

public class ContextoJson
{
    private readonly string caminhoArquivoDados;

    public List<Fornecedor> Fornecedores { get; set; } = [];
    public List<Medicamento> Medicamentos { get; set; } = [];
    public List<Paciente> Pacientes { get; set; } = [];
    public List<Funcionario> Funcionarios { get; set; } = [];
    public List<RequisicaoEntrada> RequisicoesEntrada { get; set; } = [];
    public List<RequisicaoSaida> RequisicoesSaida { get; set; } = [];

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorioAplicativo = Path.Join(caminhoAppData, "ControleDeMedicamentos-Backend");

        Directory.CreateDirectory(caminhoDiretorioAplicativo);

        caminhoArquivoDados = Path.Join(caminhoDiretorioAplicativo, "dados.json");
    }

    public void Salvar()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, options);

        File.WriteAllText(caminhoArquivoDados, jsonString);
    }
    public void Carregar()
    {
        if (!File.Exists(caminhoArquivoDados))
        {
            Carregar(CarregarDadosPredefinidos());
            return;
        }

        string jsonString = File.ReadAllText(caminhoArquivoDados);

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo =
            JsonSerializer.Deserialize<ContextoJson>(jsonString, options);

        if (contextoSalvo == null || !contextoSalvo.PossuiDados())
            contextoSalvo = CarregarDadosPredefinidos();

        Carregar(contextoSalvo);
    }

    private void Carregar(ContextoJson contexto)
    {
        Fornecedores = contexto.Fornecedores;
        Medicamentos = contexto.Medicamentos;
        Pacientes = contexto.Pacientes;
        Funcionarios = contexto.Funcionarios;
        RequisicoesEntrada = contexto.RequisicoesEntrada;
        RequisicoesSaida = contexto.RequisicoesSaida;
    }
    public ContextoJson CarregarDadosPredefinidos()
    {
        ContextoJson contextoPredefinido = new ContextoJson();

        contextoPredefinido.Fornecedores =
        [
            new Fornecedor("Distribuidora Vida", "(48) 99101-1001", "12.345.678/0001-01") { Id = 1 },
            new Fornecedor("Saude Farma", "(48) 99102-1002", "23.456.789/0001-02") { Id = 2 },
            new Fornecedor("Medic Sul", "(48) 99103-1003", "34.567.890/0001-03") { Id = 3 },
            new Fornecedor("Bem Estar Produtos", "(48) 99104-1004", "45.678.901/0001-04") { Id = 4 },
            new Fornecedor("Hospitalar Brasil", "(48) 99105-1005", "56.789.012/0001-05") { Id = 5 }
        ];

        contextoPredefinido.Medicamentos =
        [
            new Medicamento("Dipirona", "Analgésico e antitérmico em comprimidos.", contextoPredefinido.Fornecedores[0]) { Id = 1 },
            new Medicamento("Amoxicilina", "Antibiótico de uso oral em cápsulas.", contextoPredefinido.Fornecedores[1]) { Id = 2 },
            new Medicamento("Ibuprofeno", "Anti-inflamatório e analgésico em comprimidos.", contextoPredefinido.Fornecedores[2]) { Id = 3 },
            new Medicamento("Loratadina", "Antialérgico de uso oral em comprimidos.", contextoPredefinido.Fornecedores[3]) { Id = 4 },
            new Medicamento("Omeprazol", "Protetor gástrico de uso oral em cápsulas.", contextoPredefinido.Fornecedores[4]) { Id = 5 }
        ];

        contextoPredefinido.Pacientes =
        [
            new Paciente("Jefferson Da Silva", "(48) 99201-2001", "100000000000001", "10000000001") { Id = 1 },
            new Paciente("Katia Chini", "(48) 99202-2002", "100000000000002", "10000000002") { Id = 2 },
            new Paciente("Carla Mendes", "(48) 99203-2003", "100000000000003", "10000000003") { Id = 3 },
            new Paciente("Diego Costa", "(48) 99204-2004", "100000000000004", "10000000004") { Id = 4 },
            new Paciente("Walmir Junior", "(48) 99205-2005", "100000000000005", "10000000005") { Id = 5 }
        ];

        contextoPredefinido.Funcionarios =
        [
            new Funcionario("Marcos Lima", "(48) 99301-3001", "20000000001") { Id = 1 },
            new Funcionario("Natalia Rocha", "(48) 99302-3002", "20000000002") { Id = 2 },
            new Funcionario("Otavio Alves", "(48) 99303-3003", "20000000003") { Id = 3 },
            new Funcionario("Patricia Souza", "(48) 99304-3004", "20000000004") { Id = 4 },
            new Funcionario("Rafael Gomes", "(48) 99305-3005", "20000000005") { Id = 5 }
        ];

        contextoPredefinido.RequisicoesEntrada =
        [
            new RequisicaoEntrada(contextoPredefinido.Medicamentos[0], 100, contextoPredefinido.Funcionarios[0]) { Id = 1, Data = new DateTime(2026, 1, 10) },
            new RequisicaoEntrada(contextoPredefinido.Medicamentos[1], 80, contextoPredefinido.Funcionarios[1]) { Id = 2, Data = new DateTime(2026, 1, 15) },
            new RequisicaoEntrada(contextoPredefinido.Medicamentos[2], 120, contextoPredefinido.Funcionarios[2]) { Id = 3, Data = new DateTime(2026, 2, 5) },
            new RequisicaoEntrada(contextoPredefinido.Medicamentos[3], 60, contextoPredefinido.Funcionarios[3]) { Id = 4, Data = new DateTime(2026, 2, 20) },
            new RequisicaoEntrada(contextoPredefinido.Medicamentos[4], 90, contextoPredefinido.Funcionarios[4]) { Id = 5, Data = new DateTime(2026, 3, 3) }
        ];

        contextoPredefinido.RequisicoesSaida =
        [
            new RequisicaoSaida(contextoPredefinido.Pacientes[0], [new MedicamentoPrescrito(contextoPredefinido.Medicamentos[0], 10)]) { Id = 1, Data = new DateTime(2026, 3, 10) },
            new RequisicaoSaida(contextoPredefinido.Pacientes[1], [new MedicamentoPrescrito(contextoPredefinido.Medicamentos[1], 8)]) { Id = 2, Data = new DateTime(2026, 3, 12) },
            new RequisicaoSaida(contextoPredefinido.Pacientes[2], [new MedicamentoPrescrito(contextoPredefinido.Medicamentos[2], 12)]) { Id = 3, Data = new DateTime(2026, 3, 15) },
            new RequisicaoSaida(contextoPredefinido.Pacientes[3], [new MedicamentoPrescrito(contextoPredefinido.Medicamentos[3], 6)]) { Id = 4, Data = new DateTime(2026, 3, 18) },
            new RequisicaoSaida(contextoPredefinido.Pacientes[4], [new MedicamentoPrescrito(contextoPredefinido.Medicamentos[4], 9)]) { Id = 5, Data = new DateTime(2026, 3, 20) }
        ];

        return contextoPredefinido;
    }

    private bool PossuiDados()
    {
        return Fornecedores.Count > 0
            || Medicamentos.Count > 0
            || Pacientes.Count > 0
            || Funcionarios.Count > 0
            || RequisicoesEntrada.Count > 0
            || RequisicoesSaida.Count > 0;

    }
}
