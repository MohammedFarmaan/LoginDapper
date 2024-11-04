using api.Models;
using api.Repository;
using api.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddControllers();

// Dependency Injection of Dapper with database
builder.Services.AddTransient<DapperDBContext>();
// Dependency injection of User Repository
builder.Services.AddTransient<IUserRepository, UserRepository>();
// Dependency Injection of Password Hashing service
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();

// Enable CORS
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();

// Enable CORS
app.UseCors(builder => 
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);

app.Run();
