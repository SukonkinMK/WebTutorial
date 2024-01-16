using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WebTutorial.Abstractions;
using WebTutorial.Models;
using WebTutorial.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProFile));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(contaierBuilder =>
{
    contaierBuilder.RegisterType<StoreRepository>().As<IStoreRepository>();
});
// builder.Services.AddSingleton<IProductRepository,ProductRepository>();
builder.Services.AddMemoryCache(o => o.TrackStatistics = true);

var config = new ConfigurationBuilder();
config.AddJsonFile("appsettings.json");
var cfg = config.Build();
builder.Host.ConfigureContainer<ContainerBuilder>(cb => cb.Register(c => new StoreContext(cfg.GetConnectionString("db"))).InstancePerDependency());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
Directory.CreateDirectory(staticFilesPath);
app.UseStaticFiles(new StaticFileOptions() { FileProvider = new PhysicalFileProvider(staticFilesPath), RequestPath = "/static" });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
