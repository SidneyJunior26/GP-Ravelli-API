using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RavelliCompete.Infra.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavelliCompete.Controllers.RegistrosMedicos;

[ApiController]
[Route("v1/[controller]")]
public class RegistrosMedicosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RegistrosMedicosController(ApplicationDbContext context) {
        _context = context;
    }

    /// <summary>
    /// Consulta os Registros Médicos de um Atleta
    /// </summary>
    /// <param name="idAtleta">ID do Atleta</param>
    /// <returns></returns>
    /// <response code="200">Retorna os Registros Médicos do Atleta com sucesso.</response>
    /// <response code="404">Retorna que não existe Registro Médico deste Atleta na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{idAtleta}")]
    [Authorize]
    public IResult ConsultarRegistro(string idAtleta) {
        var response = _context.RegistrosMedicos.FirstOrDefault(m => m.IdAtleta == idAtleta);

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

