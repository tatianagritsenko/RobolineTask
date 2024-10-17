using Microsoft.EntityFrameworkCore;
using RobolineTestTask.Database;

var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения к базе данных из файла конфигурации
string connection = builder.Configuration.GetConnectionString("RobolineDBString");
//string connection = builder.Configuration.GetConnectionString("MyDataBaseString");

// Add services to the container.

// добавляем контекст DatabaseContext в качестве сервиса в приложение
builder.Services.AddDbContext<RobolineContext>(options => options.UseSqlite(connection));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
