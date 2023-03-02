using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavelliCompete.Controllers.Atletas;

[ApiController]
[Route("v1/[controller]")]
public class AtletasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    private const int paginaTamanhoPadrao = 10;

    public AtletasController(ApplicationDbContext context, IConfiguration config) {
        _context = context;
        _config = config;
    }

    /* GET */

    /// <summary>
    /// Obtém uma lista de todos os atletas.
    /// </summary>
    /// <response code="200">Retorna os dados do Atleta.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult ConsultarTodosAtletas() {
        var response = _context.Atletas.OrderBy(a => a.Nome.Trim()).ToList();

        if (response == null)
            return NotFound(new { message = "Nenhum usuário encontrado" });

        return Ok(response);
    }

    /// <summary>
    /// Obtém uma lista de todos os atletas por nome.
    /// </summary>
    /// <response code="200">Retorna os dados do Atleta.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("nome/{nome}")]
    [AllowAnonymous]
    public IActionResult ConsultarAtletasPorNome(string nome) {
        var response = _context.Atletas.Where(a => a.Nome.Contains(nome)).ToList();

        if (response == null)
            return NotFound(new { message = "Nenhum usuário encontrado" });

        return Ok(response);
    }
    /// <summary>
    /// Obtém uma lista de todos os atletas por CPF.
    /// </summary>
    /// <response code="200">Retorna os dados do Atleta.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("cpf/{cpf}")]
    [AllowAnonymous]
    public IActionResult ConsultarAtletasPorCPF(string cpf) {
        var response = _context.Atletas.Where(a => a.Cpf.Contains(cpf)).ToList();

        if (response == null)
            return NotFound(new { message = "Nenhum usuário encontrado" });

        return Ok(response);
    }

    /// <summary>
    /// Retorna se o usuário existe
    /// </summary>
    /// <param name="cpf">CPF do atleta</param>
    /// <response code="200">Retorna que o Atleta existe na base de dados.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("existe/{cpf}")]
    [AllowAnonymous]
    public async Task<IActionResult> VerificaAtletaExiste([FromRoute] string cpf) {

        var athlete = await _context.Atletas.FirstOrDefaultAsync(a => a.Cpf == cpf);

        if (athlete == null)
            return NotFound(new { message = "Usuário não encontrado" });

        return Ok();
    }



    /// <summary>
    /// Retorna informações de um atleta específico
    /// </summary>
    /// <param name="cpf">CPF do atleta</param>
    /// <response code="200">Retorna os dados do Atleta com sucesso.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("consultar/{cpf}")]
    [Authorize]
    public async Task<IActionResult> ConsultarAtletaPorCpf([FromRoute] string cpf) {

        var athlete = await _context.Atletas.FirstOrDefaultAsync(a => a.Cpf == cpf);

        if (athlete == null)
            return NotFound(new { message = "Usuário não encontrado" });

        return Ok(athlete);
    }

    /* POST */

    /// <summary>
    /// Cadastra um novo Atleta
    /// </summary>
    /// <param name="athleteRequest">Dados do Atleta</param>
    /// <response code="201">Retorna os dados do Atleta criado com sucesso.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> CadastrarAtleta(AtletaRequest athleteRequest) {

        if (!await AthleteExists(athleteRequest)) {
            return NotFound(new { message = "Usuário não encontrado"});
        }

        var newAthlete = new Domain.Atletas.Atleta(athleteRequest);

        await _context.Atletas.AddAsync(newAthlete);
        await _context.SaveChangesAsync();

        return Created($"/atletas/{newAthlete.Id}", newAthlete.Id);
    }

    /* PUT */

    /// <summary>
    /// Atualiza os dados do Atleta
    /// </summary>
    /// <param name="athleteRequest">Dados do Atleta</param>
    /// <param name="Cpf">CPF do Atleta</param>
    /// <response code="201">Retorna os dados do Atleta atualizado com sucesso.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("{cpf}")]
    [Authorize]
    public async Task<IActionResult> AtualizarAtleta([FromRoute] string Cpf, AtletaRequest athleteRequest) {

        var athlete = _context.Atletas.FirstOrDefault(c => c.Cpf == Cpf);

        if (athlete == null)
            return NotFound(new { message = "Usuário não encontrado" });

        athlete.EditAthlete(athleteRequest);

        if (!athlete.IsValid)
            return (IActionResult)Results.ValidationProblem(athlete.Notifications.ConvertToProblemDetails());

        await _context.SaveChangesAsync();

        return Ok(athlete);
    }

    /* DELETE */

    /// <summary>
    /// Deleta o Atleta
    /// </summary>
    /// <param name="id">ID do Atleta</param>
    /// <returns></returns>
    /// <response code="200">Retorna que o Atleta foi excluído com sucesso.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> DeletarAtleta([FromRoute] string id) {

        var athlete = _context.Atletas.FirstOrDefault(c => c.Id == id);

        if (athlete == null)
            return NotFound(new { message = "Usuário não encontrado" });

        _context.Atletas.Remove(athlete);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private async Task<bool> AthleteExists(AtletaRequest athleteRequest) {
        var athlete = await _context.Atletas.FirstOrDefaultAsync(a => a.Cpf == athleteRequest.Cpf ||
                                                                         a.Rg == athleteRequest.Rg ||
                                                                         a.Email == athleteRequest.Email);

        return athlete != null;
    }
}

