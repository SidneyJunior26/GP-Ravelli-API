using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RavelliCompete.Domain.Eventos;
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
    [Authorize(Policy = "EmployeePolicy")]
    public IActionResult ConsultarEventos() {
        var eventos = _context.Eventos.OrderByDescending(e => e.Data).ToList();

        if (eventos == null)
            return NotFound();

        return Ok(eventos);
    }

    /// <summary>
    /// Consulta Eventos com filtros
    /// </summary>
    /// <returns></returns>
    /// <param name="eventoAtivo">Evento Ativo</param>
    /// <param name="inscricoesAtivas">Inscrições Ativas</param>
    /// <response code="200">Retorna lista de Eventos filtrados com sucesso.</response>
    /// <response code="404">Retorna que não existe Eventos com os filtros informados na base de dados.</response>
    [HttpGet]
    [Route("{eventoAtivo}/{inscricoesAtivas}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult ConsultarEventosAtivos(bool eventoAtivo, bool inscricoesAtivas) {
        var eventos = _context.Eventos.Where(e => e.AtivaEvento == eventoAtivo &&
                                                  e.AtivaInscricao == inscricoesAtivas &&
                                                  e.DataFimInscricao >= DateTime.Today &&
                                                  e.DataIniInscricao <= DateTime.Today)
                                                  .ToList();

        if (!eventos.Any())
            return NotFound();

        return Ok(eventos);
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
    public IActionResult ConsultarProximosEventos() {
        var eventos = _context.Eventos.Where(e => e.DataIniInscricao > DateTime.Now && e.AtivaEvento == true)
                                                  .OrderByDescending(e => e.DataIniInscricao)
                                                  .ToList();

        if (!eventos.Any())
            return NotFound();

        return Ok(eventos);
    }

    /// <summary>
    /// Consulta Evento pelo ID
    /// </summary>
    /// <returns></returns>
    /// <param name="id">ID do Evento</param>
    /// <response code="200">Retorna o Evento filtrado com sucesso.</response>
    /// <response code="404">Retorna que não existe Evento com o ID informado na base de dados.</response>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult ConsultarEvento(int id) {
        var evento = _context.Eventos.FirstOrDefault(e => e.Id == id);

        if (evento == null)
            return NotFound();

        return Ok(evento);
    }

    /// <summary>
    /// Deleta Evento pelo ID
    /// </summary>
    /// <returns></returns>
    /// <param name="id">ID do Evento</param>
    /// <response code="200">Retorna que o Evento foi deletado com sucesso.</response>
    /// <response code="404">Retorna que não existe Evento com o ID informado na base de dados.</response>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> DeletarEvento(int id) {
        var evento = _context.Eventos.FirstOrDefault(c => c.Id == id);

        if (evento == null)
            return NotFound();

        _context.Eventos.Remove(evento);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /* POTS */

    /// <summary>
    /// Cadastra Novo Evento
    /// </summary>
    /// <param name="eventoRequest">Dados do Evento</param>
    /// <response code="201">Retorna que o Evento foi criado com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<IActionResult> CadastrarEvento([FromBody] EventoRequest eventoRequest) {
        var novoEvento = new Evento(eventoRequest);

        await _context.Eventos.AddAsync(novoEvento);
        await _context.SaveChangesAsync();

        return Created($"/eventos/{novoEvento.Id}", novoEvento);
    }
}

