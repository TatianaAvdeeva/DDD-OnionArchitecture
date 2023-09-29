using System.Reflection;
using DDDTemplate.Application;
using DDDTemplate.Domain;
using DDDTemplate.Infrastructure;
using Kernel.Base.Startup;

LoggingStartup.ConfigureBootstrapLogging();
var builder = WebApplication.CreateBuilder(args);

Assembly[] assemblies =
{
    typeof(ApplicationAssemblyScanMarker).Assembly,
    typeof(InfrastructureAssemblyScanMarker).Assembly, 
    typeof(DomainAssemblyScanMarker).Assembly
};

// configure Services
Kernel.Base.Startup.StartupBase.ConfigureStartupServices(builder, assemblies);

// specific this bounded context
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomain();

var app = builder.Build();

Kernel.Base.Startup.StartupBase.ConfigureApp(app);

if (builder.Environment.IsDevelopment())
{
    await app.RunMigrationAsync(); 
}

app.Run();
