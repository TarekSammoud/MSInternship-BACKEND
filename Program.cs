using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using MSS.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MssolutionsContext>(options =>
{
        options.UseSqlServer("server = DESKTOP-TM9KI0C;database=MSSolutions; trusted_connection = true;TrustServerCertificate=true");
        string? x = builder.Configuration.GetConnectionString("DefaultConnection");
    }
);

builder.Services.AddCors(options => options.AddPolicy(name: "UserOrigins",
    policy=>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("UserOrigins"); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();