using Minimal_Api.Domain.DTOs;

namespace Minimal_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.MapPost("/login", (LoginDTO loginDTO) => 
            {
                if (loginDTO.Email == "adm@teste.com" && loginDTO.Senha == "123456") 
                {
                    return Results.Ok("Login com sucesso!");
                }
                else
                {
                    return Results.Unauthorized();
                }
            });

            app.Run();
        }
    }
}
