using ApiSakila.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSakila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly SakilaDbContext _dbContext;
        public LanguageController(SakilaDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Realizar una consulta de prueba
                var primerRegistro = _dbContext.Languages.FirstOrDefault();

                if (primerRegistro != null)
                {
                    return Ok(primerRegistro);
                }
                else
                {
                    return NotFound("No se encontraron registros.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

