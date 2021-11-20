namespace DotNet.DIInConsole;
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