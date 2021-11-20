using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DIInConsole
{
    public class Worker
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly DbCtxt _dbCtxt;

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
}