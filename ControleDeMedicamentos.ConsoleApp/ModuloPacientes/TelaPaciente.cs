using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaOpcoes, ITelaCrud
{
    public TelaPaciente(string nomeEntidade, RepositorioBaseEmArquivo<Paciente> repositorio) : base(nomeEntidade, repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        throw new NotImplementedException();
    }

    protected override Paciente ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do paciente: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o número do cartão do SUS do Paciente: ");
        string cartaoSus = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CPF do paciente: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Paciente(nome, telefone, cartaoSus, cpf);
    }
}
