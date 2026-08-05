// Objetivo: Rodar um servidor web
// Servidor Web: um programa que executa na rede local/remota
// ... e espera por requisicoes externas...
// ... geralmente responde com arquivos HTML / CSS / JS (Paginas WEB)

// Objeto de configuraçao do servidor
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Habilita o MVC = Model - View - Controller
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Middlewares - funçoes que executam a cada execuçao e resposta
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

// Executa o servidor
app.Run();
