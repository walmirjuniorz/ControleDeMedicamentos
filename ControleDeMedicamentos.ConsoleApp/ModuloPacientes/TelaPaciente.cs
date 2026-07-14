using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaOpcoes, ITelaCrud
{
    public TelaPaciente(RepositorioPacienteEmArquivo repositorio) : base("Paciente", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            //Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Pacientes");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -18} | {4, -13}",
            "Id", "Nome", "Telefone", "Cartao do SUS", "CPF"
        );

        List<Paciente> pacientes = repositorio.SelecionarTodos();

        foreach (Paciente p in pacientes)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -18} | {4, -13}",
                p.Id, p.Nome, p.Telefone, p.CartaoSus, p.Cpf
            );
        }
        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }
    protected override Paciente ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do paciente: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o número do cartão do SUS do paciente: ");
        string cartaoSus = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CPF do paciente: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Paciente(nome, telefone, cartaoSus, cpf);
    }
    protected override bool ExisteRegistroComInformacoesExclusivas(Paciente entidade, int? idIgnorado = null)
    {
        List<Paciente> registros = repositorio.SelecionarTodos();

        foreach (Paciente p in registros)
        {
            if (p.Id != idIgnorado && p.CartaoSus == entidade.CartaoSus)
            {
                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um paciente cadastrado com o cartao do SUS informado!");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");
                return true;
            }
        }
        return false;
    }
}
