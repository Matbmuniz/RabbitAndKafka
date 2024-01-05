using App.Repository;
using App.Repository.Interfaces;
using App.Services;
using App.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IAppMessageService, AppMessageService>();

//Para utilizar o RabbitMQ
//builder.Services.AddTransient<IAppMessageRepository, RabbitMessageRepository>();
// -----------------------
//Para utilizar o Kafka
builder.Services.AddTransient<IAppMessageRepository, KafkaMessageRepository>();

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
