using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RavelliCompete.Infra.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavelliCompete.Controllers.Eventos;

[ApiController]
[Route("v1/[controller]")]
public class EventosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventosController(ApplicationDbContext context) {
        _context = context;
    }

    /// <summary>
    /// Consulta todos os eventos
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna lista de Eventos com sucesso.</response>
    /// <response code="404">Retorna que não existe Eventos na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult ConsultarEventos() {
        var response = _context.Eventos.OrderByDescending(e => e.Data).ToList();

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>
    /// Consulta Eventos com filtros
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna lista de Eventos filtrados com sucesso.</response>
    /// <response code="404">Retorna que não existe Eventos com os filtros informados na base de dados.</response>
    [HttpGet]
    [Route("{active:bool}/{subscription:bool}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult ConsultarEventosAtivos(bool active, bool subscription) {
        var response = _context.Eventos.Where(e => e.AtivaEvento == active &&
                                                  e.AtivaInscricao == subscription &&
                                                  e.DataFimInscricao >= DateTime.Now &&
                                                  e.DataIniInscricao <= DateTime.Now)
                                                  .ToList();

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>
    /// Consulta Eventos com filtros
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna lista de Eventos filtrados com sucesso.</response>
    /// <response code="404">Retorna que não existe Eventos com os filtros informados na base de dados.</response>
    [HttpGet]
    [Route("proximos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult ConsultarProximosEventos(bool active, bool subscription) {
        var response = _context.Eventos.Where(e => e.DataIniInscricao > DateTime.Now)
                                                  .OrderByDescending(e => e.DataIniInscricao)
                                                  .ToList();

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>
    /// Consulta Evento pelo ID
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna o Evento filtrado com sucesso.</response>
    /// <response code="404">Retorna que não existe Evento com o ID informado na base de dados.</response>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult ConsultarEvento(int id) {
        var response = _context.Eventos.FirstOrDefault(e => e.Id == id);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>
    /// Deleta Evento pelo ID
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna que o Evento foi deletado com sucesso.</response>
    /// <response code="404">Retorna que não existe Evento com o ID informado na base de dados.</response>
    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> DeletarEventoAsync(int id) {
        var eventInstance = _context.Eventos.FirstOrDefault(c => c.Id == id);

        if (eventInstance == null)
            return NotFound();

        _context.Eventos.Remove(eventInstance);
        await _context.SaveChangesAsync();

        return Ok();
    }
}

