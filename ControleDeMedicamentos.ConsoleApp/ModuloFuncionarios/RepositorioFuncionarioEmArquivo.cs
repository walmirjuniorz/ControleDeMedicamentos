using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Paciente>
{
    public RepositorioFuncionarioEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Paciente> ObterRegistros()
    {
        throw new NotImplementedException();
    }
}
