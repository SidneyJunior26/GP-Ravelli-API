using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Security;

namespace RavelliCompete.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/athletes/login";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action(LoginRequest loginRequest, ApplicationDbContext context, IConfiguration config) {
        var userOk = context.Atletas.FirstOrDefault(a => a.Cpf == loginRequest.Cpf);

        if (userOk == null)
            return Results.NotFound();

        if (userOk.Acesso != loginRequest.Password)
            return Results.Unauthorized();

        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim("ID", userOk.Id),
            new Claim("cpf", loginRequest.Cpf),
            new Claim("Name", userOk.Nome)
        });

        var tokenDescription = new SecurityService(config).GetTokenDescriptor(subject);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);

        return Results.Ok(new {
            token = tokenHandler.WriteToken(token)
        });
    }
}

