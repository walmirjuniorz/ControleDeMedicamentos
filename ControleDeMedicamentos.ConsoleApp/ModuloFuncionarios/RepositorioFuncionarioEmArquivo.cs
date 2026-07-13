using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>
{
    public RepositorioFuncionarioEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Funcionario> ObterRegistros()
    {
        return contexto.Funcionarios;
    }
}
