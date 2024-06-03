
using Dapper.FluentMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PipelinePlayground;
using PipelinePlayground.Configurations;
using PipelinePlayground.Infrastructure.Data.Contexts;
using PipelinePlayground.Infrastructure.Data.Factories;
using PipelinePlayground.Infrastructure.Data.Mapping.Dapper;

var serviceCollection = new ServiceCollection();

Configure(serviceCollection);
AddServices(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();
var app = serviceProvider.GetRequiredService<IApp>();

FluentMapper.Initialize(config =>
{
    config.AddMap(new UserMap());
});

await app.Run();

static void Configure(ServiceCollection serviceCollection)
{
    serviceCollection
        .AddSingleton<IConfiguration>(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build());
}

static void AddServices(ServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<IApp, App>(sp =>
    {
        var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Odbc");

        var odbcContext = sp.GetRequiredService<IOdbcContext>();
        var app = new App(odbcContext, () => new OdbcConfiguration
        {
            ConnectionString = connectionString!
        });

        return app;
    });
    serviceCollection.AddScoped<IOdbcConnectionFactory, OdbcConnectionFactory>();
    serviceCollection.AddSingleton<IOdbcContext, OdbcContext>();

    //public delegate TResult Func<out TResult>(); -> does not take arguments just a return. If an Action, then should not has a return, just arguments.
}