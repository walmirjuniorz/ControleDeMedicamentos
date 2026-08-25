// Objetivo: Rodar um servidor web
// Servidor Web: um programa que executa na rede local/remota
// ... e espera por requisicoes externas...
// ... geralmente responde com arquivos HTML / CSS / JS (Paginas WEB)

// Objeto de configuraçao do servidor
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Adiciona e injeta apenas UMA instancia requisitada.
// builder.Services.AddSingleton();

// Adiciona e injeta uma instancia por requisição/conexão.
// Func<IServiceProvider, ContextoJson> implementationFactory

builder.Services.AddScoped<ContextoJson>(ContextoJson.InjetarContexto);
builder.Services.AddScoped<RepositorioMedicamentoEmArquivo>();
builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();
builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>();
builder.Services.AddScoped<RepositorioPacienteEmArquivo>();
builder.Services.AddScoped<RepositorioRequisicaoEntradaEmArquivo>();
builder.Services.AddScoped<RepositorioRequisicaoSaidaEmArquivo>();

// Adiciona e injeta uma instancia por ocorrencia da dependencia.
// builder.Services.AddTransient();

// Habilita o MVC = Model - View - Controller
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Middlewares - funçoes que executam a cada execuçao e resposta
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

// Executa o servidor
app.Run();
