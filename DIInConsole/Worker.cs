namespace DotNet.DIInConsole;
public class Worker
{
    private readonly IConfiguration _configuration;
    private readonly DbCtxt _dbCtxt;
    private readonly ILogger<Worker> _logger;

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