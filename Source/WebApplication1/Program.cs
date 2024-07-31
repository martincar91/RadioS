
using RadioS.Application;
using RadioS.Application.Contracts;
using RadioS.Application.Models;
using RadioS.Persistence;
using RadioS.Persistence.MockData;
using RadioS.Proxies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IAssemblyReference).Assembly));
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IWhiteListService, TenantWhiteListService>();
builder.Services.AddScoped<IWhiteListService, ClientWhiteListService>();
builder.Services.AddScoped<IWhiteListServiceStrategy, WhiteListServiceStrategy>();
builder.Services.AddScoped<ISupportedProductService, SupportedProductsService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.Configure<AnonymizationSettings>(builder.Configuration.GetSection("AnonymizationSettings"));
builder.Services.AddScoped<IAnonymizeService, AnonymizeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
