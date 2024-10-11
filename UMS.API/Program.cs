using FileManager.DataAccessLayer.DataContext;
using Project_G2.BuissnessAccessLayer.Services.IServices;
using Project_G2.BuissnessAccessLayer.Services;
using Project_G2.DataAccessLayer.Repository.IRepository;
using Project_G2.DataAccessLayer.Repository;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddTransient<DapperDBContext>();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

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

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithHeaders(HeaderNames.ContentType);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
