using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace redis_csharp
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      using (var host = CreateHostBuilder(args).Build())
      {
        await host.RunAsync();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host
          .CreateDefaultBuilder(args)
          .UseServiceProviderFactory(new AutofacServiceProviderFactory())
          .ConfigureContainer<ContainerBuilder>(builder =>
          {

            builder.Register(ctx =>
            {
              var opt = ctx.Resolve<IOptions<Settings>>();
              return opt.Value;
            });

            builder.Register(ctx =>
            {
              var settings = ctx.Resolve<Settings>();
              return settings.SettingsData;
            });

            builder.Register(ctx =>
            {
              var settings = ctx.Resolve<SettingsData>();
              return settings.RedisConnectionString;
            });
          })
          .ConfigureServices((context, services) =>
          {
            services
              .AddOptions()
              .Configure<Settings>(context.Configuration)
              .AddLogging();
            services.AddHostedService<ProducerWorker>();
          })
          ;
  }
}
