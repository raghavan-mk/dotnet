namespace DotNet.DIInConsole;
public class DbCtxt
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbCtxt> _logger;
    public DbCtxt(IConfiguration configuration, ILogger<DbCtxt> logger)
    {
        _configuration = configuration;
        _logger = logger;
        //read db config
    }
    public void DoSomeDbStuff()
    {
        _logger.LogInformation($"Doing some {nameof(DoSomeDbStuff)}");
    }
}