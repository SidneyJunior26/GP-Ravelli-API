using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RavelliCompete.Infra.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavelliCompete.Controllers.SubCategorias;

[ApiController]
[Route("v1/[controller]")]
public class SubCategoriaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubCategoriaController(ApplicationDbContext context) {
        _context = context;
    }

    /// <summary>
    /// Consulta as Categorias do Evento
    /// </summary>
    /// <param name="idEvento">ID do Evento</param>
    /// <param name="categoria">Circuito do Evento</param>
    /// <param name="idade">Idade do Atleta</param>
    /// <param name="sexo">Sexo do Atleta</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{idEvento}/{categoria}/{idade}/{sexo}")]
    [Authorize]
    public IActionResult ConsultarCategorias(int idEvento, int categoria, int idade, int sexo) {
        var response = _context.Subcategoria.Where(s => s.IdEvento == idEvento
                                                       && s.Categoria == categoria
                                                       && s.IdadeAte >= idade
                                                       && s.IdadeDe <= idade
                                                       && s.FiltroSexo == sexo
                                                       && s.Ativo == true).ToList();

        if (response == null)
            return NotFound();

        return Ok(response);
    }
}

