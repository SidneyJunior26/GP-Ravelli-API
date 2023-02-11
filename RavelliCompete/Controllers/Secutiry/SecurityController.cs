using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using RavelliCompete.Endpoints.Security;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Security;
using static Dapper.SqlMapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavelliCompete.Controllers.Secutiry;

[ApiController]
[Route("v1/seguranca")]
public class SecurityController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public SecurityController(ApplicationDbContext context, IConfiguration config) {
        _context = context;
        _config = config;
    }

    /// <summary>
    /// Obtem o token JWT para Login do Atleta
    /// </summary>
    /// <param name="loginRequest">Dados do Atleta para login</param>
    /// <response code="200">Retorna o tokwn JWT com sucesso.</response>
    /// <response code="401">Retorna que a senha do usuário está inválida.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest) {
        var userOk = _context.Atletas.FirstOrDefault(a => a.Cpf == loginRequest.Cpf);

        if (userOk == null)
            return NotFound();

        if (userOk.Acesso != loginRequest.Password)
            return Unauthorized();

        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim("ID", userOk.Id),
            new Claim("cpf", loginRequest.Cpf),
            new Claim("Name", userOk.Nome)
        });

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["JwtBearerTokenSettings:SecretKey"]);
        var tokenDescription = new SecurityTokenDescriptor {
            Subject = subject,
            Audience = _config["JwtBearerTokenSettings:Audience"],
            Issuer = _config["JwtBearerTokenSettings:Issuer"],
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescription);

        return Ok(new {
            token = tokenHandler.WriteToken(token)
        });
    }
}

