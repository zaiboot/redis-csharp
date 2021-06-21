using System.Threading;
using System.Threading.Tasks;
using FreeRedis;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace redis_csharp
{
  public class ConsumerWorker : BackgroundService
  {
    private RedisClient client;
    private readonly ConnectionStringBuilder builder;
    private readonly ILogger<ConsumerWorker> logger;
    private const string KEY_NAME = "Test01";

    public ConsumerWorker(ConnectionStringBuilder builder, ILogger<ConsumerWorker> logger)
    {
      client = new RedisClient(builder);
      this.builder = builder;
      this.logger = logger;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
      client.Dispose();
      return Task.CompletedTask;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        var value = client.RPop(KEY_NAME);
        logger.LogInformation("New item popped -> {0}", value);
        Thread.Sleep(500);
      }
      return Task.CompletedTask;
    }
  }
}