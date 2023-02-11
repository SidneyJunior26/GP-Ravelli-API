using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RavelliCompete.Domain.Eventos;
using RavelliCompete.Infra.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavelliCompete.Controllers.Regulamentos;

[ApiController]
[Route("v1/[controller]")]
public class RegulamentosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RegulamentosController(ApplicationDbContext context) {
        _context = context;
    }

    /// <summary>
    /// Consulta o Regulamento do Evento
    /// </summary>
    /// <returns></returns>
    /// <param name="idEvento">ID do Evento</param>
    /// <response code="200">Retorna o Regulamento do Evento com sucesso.</response>
    /// <response code="404">Retorna que não existe Regulamento deste Evento na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{idEvento}")]
    [Authorize]
    public IActionResult ConsultarRegulamento(int idEvento) {
        var response = _context.Regulamentos.FirstOrDefault(e => e.IdEvento == idEvento);

        if (response == null)
            return NotFound();

        return Ok(response);
    }
}

