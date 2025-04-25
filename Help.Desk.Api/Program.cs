using System.Text;

using FluentValidation;
using Help.Desk.Application.Validators.UserValidators;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>(ServiceLifetime.Scoped);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Help Desk API", Version = "v1" });
    // TODO: Configurar Swagger para aceptar Token JWT (Bearer)
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication(); // Habilitar autenticación
//app.UseAuthorization();  // Habilitar autorización


app.MapControllers(); // Mapea los controladores


app.Run();