using million_api.Models.Entities;
using million_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection("MillionDatabase"));
builder.Services.AddSingleton<PropertyService>();
builder.Services.AddSingleton<OwnerService>();
builder.Services.AddSingleton<PropertyTraceService>();
builder.Services.AddSingleton<PropertyImageService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
