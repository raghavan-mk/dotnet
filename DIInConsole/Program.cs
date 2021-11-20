// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DIInConsole;

using var host = CreateHostBuilder(args).Build();
await host.StartAsync();
using var scope = host.Services.CreateScope();
var worker = scope.ServiceProvider.GetService<Worker>();
await worker!.Run();
await host.StopAsync();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices(services => ConfigureServices(services));

void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<Worker>();
    services.AddScoped<DbCtxt>();
}