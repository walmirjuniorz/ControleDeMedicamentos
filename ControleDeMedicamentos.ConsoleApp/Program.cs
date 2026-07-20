// Objetivo: Rodar um servidor web
// Servidor Web: um programa que executa na rede local/remota
// ... e espera por requisicoes externas...
// ... geralmente responde com arquivos HTML / CSS / JS (Paginas WEB)

// Objeto de configuraçao do servidor
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

// Executa o servidor
app.Run();
