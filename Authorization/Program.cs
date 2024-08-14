using Authorization.Models.DAL.Authority;
using Authorization.Models.Logics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; 

// Add services to the container.
builder.Services.AddDbContext<authContext>(options => options.UseSqlServer(configuration.GetConnectionString("AuthorityConnection")));
builder.Services.AddScoped<UserLogic, UserLogic>();
builder.Services.AddScoped<AppLogic, AppLogic>();
builder.Services.AddScoped<RoleLogic, RoleLogic>();
builder.Services.AddScoped<EmployeLogic, EmployeLogic>();
builder.Services.AddDbContext<authContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AuthorityConnection") + "TrustServerCertificate=True"));

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
