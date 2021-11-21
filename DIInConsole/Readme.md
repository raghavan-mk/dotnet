Injecting DI in Console application
-------------------------------
There are times when we want to use Dependency Injection and other out of the box features available with host builder in console app as well. We do not have this auto generated when we create a new console app. But it is reasonbly simple to write a few lines of code to get this working.

Steps
-----
Create a default host builder
In configure services, configure class(es) which will be needed to invoked/injected. It can done within the ConfigureServices extension method or in a separate method.

```C#

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
```
Worker Class

```C#
public class Worker
{
    private readonly IConfiguration _configuration;
    private readonly DbCtxt _dbCtxt;
    private readonly ILogger<Worker> _logger;

    //f/w will DI logging, configuration etc
    //also being DI'd is DB context class
    public Worker(IConfiguration configuration, ILogger<Worker> logger, DbCtxt dbCtxt)
    {
        _configuration = configuration;
        _logger = logger;
        _dbCtxt = dbCtxt;
    }
    public Task Run()
    {
        _dbCtxt.DoSomeDbStuff();
        return Task.CompletedTask;
    }
}
```
DB context class

```C#
public class DbCtxt
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbCtxt> _logger;
    // private readonly string _connectionString;

    public DbCtxt(IConfiguration configuration, ILogger<DbCtxt> logger)
    {
        _configuration = configuration;
        _logger = logger;
        //read db config e.g. read connection string
        // _connectionString = _configuration.GetConnectionString("MyDb");
    }
    public void DoSomeDbStuff()
    {
        _logger.LogInformation($"Doing some {nameof(DoSomeDbStuff)}");
    }
}
```
Invoke them all as part of starting the application

```C#
using var host = CreateHostBuilder(args).Build(); 
await host.StartAsync(); 

//scope may not be mandatory in this case. 
//but guess just a good practice to follow if we are invoking them outside the scope of being injected into the classes

using var scope = host.Services.CreateScope(); 
var worker = scope.ServiceProvider.GetService<Worker>();
await worker!.Run();
await host.StopAsync();
```
When we run the application

```C#
dotnet run
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /..
info: DotNet.DIInConsole.DbCtxt[0]
      Doing some DoSomeDbStuff+
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...
```
Complete code https://github.com/raghavan-mk/dotnet/tree/main/DIInConsole 
