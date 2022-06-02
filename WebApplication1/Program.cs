using KoalaKit;
using KoalaKit.Extensions;
using KoalaKit.Persistence.EFCore;
using KoalaKit.Persistence.EFCore.SqlServer;
using WebApplication1;

Console.WriteLine("*************** Startup ***************");
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine("*************** Startup ***************");
builder.Services.AddKoalaKit(builder => builder.AddModules(typeof(SqlServerModule)));
//builder.Services.AddDbContext<KoalaDbContext, WebApplicationOneDbContext>();
EntityProvidersCollection.AddDbEntityProvider(typeof(GenericDbEntityProvider).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
