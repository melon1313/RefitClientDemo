using Refit;
using RefitClientDemo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//HttpClientFactory x RefitClient
services
    .AddRefitClient<IClubClient>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5272"));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
