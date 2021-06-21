using System;
using System.Threading;
using System.Threading.Tasks;
using FreeRedis;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace redis_csharp
{
  public class ProducerWorker : BackgroundService
  {
    private RedisClient client;
    private readonly ConnectionStringBuilder builder;
    private ILogger<ProducerWorker> logger;
    private const string KEY_NAME = "Test01";

    public ProducerWorker(ConnectionStringBuilder builder, ILogger<ProducerWorker> logger)
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
        var value = "Zuegraaa - " + DateTime.UtcNow.Millisecond;
        client.RPush(KEY_NAME, value);
        logger.LogInformation("New item pushed -> {0}", value);
        Thread.Sleep(1000);
      }

      return Task.CompletedTask;
    }
  }
}