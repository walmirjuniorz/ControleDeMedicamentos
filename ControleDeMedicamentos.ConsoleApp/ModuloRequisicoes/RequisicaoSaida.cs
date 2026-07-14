using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

public class RequisicaoSaida : EntidadeBase
{
    public Paciente Paciente { get; set; }
    public Medicamento Medicamento { get; set; }
    public int Quantidade { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;

    public RequisicaoSaida() { }

    public RequisicaoSaida(Paciente paciente, Medicamento medicamento, int quantidade) : this()
    {
        Paciente = paciente;
        Medicamento = medicamento;
        Quantidade = quantidade;

        medicamento.RegistrarRequisicaoSaida(this);

    }
    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        RequisicaoSaida requisicaoSaidaAtualizada = (RequisicaoSaida)entidadeAtualizada;

        Paciente = requisicaoSaidaAtualizada.Paciente;
        Medicamento = requisicaoSaidaAtualizada.Medicamento;
        Quantidade = requisicaoSaidaAtualizada.Quantidade;
    }
    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Paciente == null)
            erros.Add("O campo \"Paciente\" deve ser preenchido.");

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" deve ser preenchido.");

        if (Quantidade <= 0)
            erros.Add("A \"Quantidade\" deve ser maior que zero.");

        return erros;
    }
}
