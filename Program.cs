global using BookEntityFramework.Models;
global using Microsoft.EntityFrameworkCore;
using BookEntityFramework.AppSetttings;
using BookEntityFramework.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Registering Repository services
builder.Services.AddRepository();
builder.Services.AddControllers();
builder.Services.Configure<JwtClaimDetails>(builder.Configuration.GetSection("Jwt"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.SwaggerAuthorization();
builder.Services.AuthenticationExt(builder.Configuration);



//DB FIRST
builder.Services.AddDbContext<LynnContext>(options=>
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString("ConnectionString")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
