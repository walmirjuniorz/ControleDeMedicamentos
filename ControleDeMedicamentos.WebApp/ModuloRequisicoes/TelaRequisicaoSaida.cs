using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;

    public TelaRequisicaoSaida(
        RepositorioRequisicaoSaidaEmArquivo repositorioRequisicao,
        RepositorioPacienteEmArquivo repositorioPaciente,
        RepositorioMedicamentoEmArquivo repositorioMedicamento)
        : base("Requisiçao de Saída", repositorioRequisicao)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioMedicamento = repositorioMedicamento;
    }
    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Requisições de Saída");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -10} |{4, -15}",
            "Id", "Paciente", "Medicamento", "Qtd", "Data"
        );

        List<RequisicaoSaida> registros = repositorio.SelecionarTodos();

        foreach (RequisicaoSaida r in registros)
        {
            List<string> nomesMedicamentos = new List<string>();
            int quantidadeTotal = 0;

            foreach (MedicamentoPrescrito medicamentoPrescrito in r.MedicamentosPrescritos)
            {
                nomesMedicamentos.Add(medicamentoPrescrito.Medicamento.Nome);
                quantidadeTotal += medicamentoPrescrito.Quantidade;
            }

            string medicamentos = string.Join(", ", nomesMedicamentos);

            Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -10} |{4, -15}",
                r.Id, r.Paciente.Nome, medicamentos, quantidadeTotal, r.Data.ToShortDateString()
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }
    protected override RequisicaoSaida ObterDadosCadastrais()
    {
        VisualizarPacientes();

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do paciente que receberá o medicamento: ");
        int idPaciente = Convert.ToInt32(Console.ReadLine());

        Paciente paciente = repositorioPaciente.SelecionarPorId(idPaciente);

        VisualizarMedicamentos();

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do medicamento: ");
        int idMedicamento = Convert.ToInt32(Console.ReadLine());

        Medicamento medicamento = repositorioMedicamento.SelecionarPorId(idMedicamento);

        Console.Write("Digite a quantidade que deseja levar: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        MedicamentoPrescrito medicamentoPrescrito = new MedicamentoPrescrito(medicamento, quantidade);
        List<MedicamentoPrescrito> medicamentosPrescritos = new List<MedicamentoPrescrito>();

        medicamentosPrescritos.Add(medicamentoPrescrito);

        return new RequisicaoSaida(paciente, medicamentosPrescritos);
    }
    private void VisualizarPacientes()
    {
        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -18} | {4, -13}",
            "Id", "Nome", "Telefone", "Cartao do SUS", "CPF"
        );

        List<Paciente> registros = repositorioPaciente.SelecionarTodos();

        foreach (Paciente p in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -18} | {4, -13}",
                p.Id, p.Nome, p.Telefone, p.CartaoSus, p.Cpf
            );
        }
    }
    private void VisualizarMedicamentos()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -12}",
            "Id", "Nome", "Fornecedor", "Descrição", "Estoque"
        );

        List<Medicamento> registros = repositorioMedicamento.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -12}",
                m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao, m.QuantidadeEmEstoque
            );
        }
    }
    protected override bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
    {
        return base.ExistemDependenciasAtivasDoRegistro(idRegistro);
    }
}
