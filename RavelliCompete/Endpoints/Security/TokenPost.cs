﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Security;

namespace RavelliCompete.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/atletas/login";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action(LoginRequest loginRequest, ApplicationDbContext context, IConfiguration config) {
        var userOk = context.Athlete.FirstOrDefault(a => a.Cpf == loginRequest.Cpf && a.Acesso == loginRequest.Password);

        if (userOk == null)
            return Results.NotFound();

        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim("ID", loginRequest.Cpf)
        });

        var tokenDescription = new SecurityService(config).GetTokenDescriptor(subject);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);

        return Results.Ok(new {
            token = tokenHandler.WriteToken(token)
        });
    }
}
