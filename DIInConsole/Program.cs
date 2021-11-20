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
    //main class which does the work
    services.AddScoped<Worker>();
    //do some DB operations
    services.AddScoped<DbCtxt>();
}