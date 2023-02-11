using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services;
using RavelliCompete.Services.Athletes;

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
    /// Obtém uma lista de todos os atletas com paginação.
    /// </summary>
    /// <param name="paginaNumero">Número da página para recuperar (opcional, padrão é 1).</param>
    /// <param name="paginaTamanho">Tamanho da página para recuperar (opcional, padrão é 10).</param>
    /// <response code="200">Retorna os dados do Atleta.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> ConsultarTodosAtletas([FromServices] QueryAllAthletesWithPagination query, int? paginaNumero, int? paginaTamanho) {

        var paginaNumeroAtual = paginaNumero ?? 1;
        var paginaTamanhoAtual = paginaTamanho ?? paginaTamanhoPadrao;

        var response = await query.Execute(paginaNumeroAtual, paginaTamanhoAtual);

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
    public async Task<IActionResult> CadastrarAtleta(AtletaPostRequest athleteRequest) {

        if (!await AthleteExists(athleteRequest)) {
            return NotFound(new { message = "Usuário não encontrado"});
        }

        var newAthlete = new RavelliCompete.Domain.Athletes.Athlete(athleteRequest);

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
    public async Task<IActionResult> AtualizarAtleta([FromRoute] string Cpf, AtletaPostRequest athleteRequest) {

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

    private async Task<bool> AthleteExists(AtletaPostRequest athleteRequest) {
        var athlete = await _context.Atletas.FirstOrDefaultAsync(a => a.Cpf == athleteRequest.Cpf ||
                                                                         a.Rg == athleteRequest.Rg ||
                                                                         a.Email == athleteRequest.Email);

        return athlete != null;
    }
}

