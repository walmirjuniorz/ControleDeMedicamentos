// Objetivo: Rodar um servidor web
// Servidor Web: um programa que executa na rede local/remota
// ... e espera por requisicoes externas...
// ... geralmente responde com arquivos HTML / CSS / JS (Paginas WEB)

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Adiciona e injeta apenas UMA instancia requisitada.
// builder.Services.AddSingleton();

// Adiciona e injeta uma instancia por requisição/conexão.

// Delegates
// Func<IServiceProvider, ContextoJson> implementationFactory

// Expressão Lambda =>

// Habilita o armazenamento em JSON

builder.Services.AddInfraestruturaEmJson();

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
