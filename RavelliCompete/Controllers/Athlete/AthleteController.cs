using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services;
using RavelliCompete.Services.Athletes;
using Swashbuckle.Swagger;
using Swashbuckle.Swagger.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavelliCompete.Controllers.Athlete;

[ApiController]
[Route("v1/athletes")]
public class AthleteController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private const int DefaultPageSize = 10;

    public AthleteController(ApplicationDbContext context) {
        _context = context;
    }

    // GET

    /// <summary>
    /// Obtém uma lista de todos os atletas com paginação.
    /// </summary>
    /// <param name="pageNumber">Número da página para recuperar (opcional, padrão é 1).</param>
    /// <param name="pageSize">Tamanho da página para recuperar (opcional, padrão é 10).</param>
    /// <response code="200">Retorna os dados do Atleta.</response>
    /// <response code="404">Atleta não encontrado.</response>
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Athletes retrieved successfully.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Athletes not found.")]
    public async Task<IActionResult> GetAllAthletes(
            int? pageNumber,
            int? pageSize,
            [FromServices] QueryAllAthletesWithPagination query) {

        var currentPageNumber = pageNumber ?? 1;
        var currentPageSize = pageSize ?? DefaultPageSize;

        var response = await query.Execute(currentPageNumber, currentPageSize);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>
    /// Retorna se o usuário existe
    /// </summary>
    /// <param name="cpf">CPF do atleta</param>
    /// <response code="200">Retorna que o Atleta existe na base de dados.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpGet]
    [Route("exists/{cpf}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Athlete exists.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Athlete not found.")]
    public async Task<IActionResult> CheckAthleteExists(string cpf) {
        var athlete = await _context.Athlete.FirstOrDefaultAsync(a => a.Cpf == cpf);

        if (athlete == null)
            return NotFound();

        return Ok();
    }

    /// <summary>
    /// Retorna informações de um atleta específico
    /// </summary>
    /// <param name="cpf">CPF do atleta</param>
    /// <response code="200">Retorna os dados do Atleta com sucesso.</response>
    /// <response code="404">Retorna que o Atleta não existe na base de dados.</response>
    [HttpGet]
    [Route("{cpf}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Athlete infos.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Athlete not found.")]
    public async Task<IActionResult> GetAthleteByCpf(string cpf) {
        var athlete = await _context.Athlete.FirstOrDefaultAsync(a => a.Cpf == cpf);

        if (athlete == null)
            return NotFound();

        return Ok(athlete);
    }

    // POST
    [HttpPost]
    [SwaggerResponse(201, "Athlete created.", typeof(void))]
    [SwaggerResponse(404, "Athlete not found.", typeof(void))]
    public async Task<IActionResult> AddAthlete(AthletePostRequest athleteRequest) {

        if (await AthleteExists(athleteRequest)) {
            return Ok(athleteRequest);
        }

        var newAthlete = new RavelliCompete.Domain.Athletes.Athlete(athleteRequest);

        await _context.Athlete.AddAsync(newAthlete);
        await _context.SaveChangesAsync();

        return Created($"/atletas/{newAthlete.Id}", newAthlete.Id);
    }

    // PUT
    [HttpPut]
    [Route("{cpf}")]
    [SwaggerResponse(201, "Athlete updated.", typeof(void))]
    [SwaggerResponse(404, "Athlete not found.", typeof(void))]
    public async Task<IActionResult> UpdateAthlete(
        string Cpf,
        AthletePostRequest athleteRequest) {
        var athlete = _context.Athlete.FirstOrDefault(c => c.Cpf == Cpf);

        if (athlete == null)
            return NotFound();

        athlete.EditAthlete(athleteRequest);

        if (!athlete.IsValid)
            return (IActionResult)Results.ValidationProblem(athlete.Notifications.ConvertToProblemDetails());

        await _context.SaveChangesAsync();

        return Ok(athlete);
    }

    // DELETE
    [HttpDelete]
    [SwaggerResponse(StatusCodes.Status200OK, "Athlete deleted successfully.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Athlete not found.")]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAthlete([FromRoute] string id) {
        var athlete = _context.Athlete.FirstOrDefault(c => c.Id == id);

        if (athlete == null)
            return NotFound();

        _context.Athlete.Remove(athlete);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private async Task<bool> AthleteExists(AthletePostRequest athleteRequest) {
        var athlete = await _context.Athlete.FirstOrDefaultAsync(a => a.Cpf == athleteRequest.Cpf ||
                                                                         a.Rg == athleteRequest.Rg ||
                                                                         a.Email == athleteRequest.Email);

        return athlete != null;
    }
}

