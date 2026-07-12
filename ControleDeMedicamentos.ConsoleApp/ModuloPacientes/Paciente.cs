using System.Text.RegularExpressions;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
/*
    ● Campos obrigatórios:
            ○ Nome (3-100 caracteres)
            ○ Telefone (formatos válidos: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
            ○ Cartão do SUS (15 dígitos)
            ○ CPF (11 dígitos)
        ● O sistema não deve permitir cadastro de pacientes com mesmo cartão do SUS
*/
public class Paciente : EntidadeBase
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSus { get; set; }
    public string Cpf { get; set; }

    public Paciente(string nome, string telefone, string cartaoSus, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }
    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" precisa ter entre 3 e 100 caracteres!");

        if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.");

        if (CartaoSus.Length != 15)
            erros.Add("O \"Cartao do SUS\" precisa ter 15 dígitos!");

        if (Cpf.Length != 11)
            erros.Add("O \"CPF\" precisa ter 11 dígitos!");

        return erros;
    }
    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Paciente pacienteAtualizado = (Paciente)entidadeAtualizada;

        Nome = pacienteAtualizado.Nome;
        Telefone = pacienteAtualizado.Telefone;
        CartaoSus = pacienteAtualizado.CartaoSus;
        Cpf = pacienteAtualizado.Cpf;
    }
}
