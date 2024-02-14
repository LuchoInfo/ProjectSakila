using ApiSakila.Models;
using ApiSakila.Models.Dto;
using ApiSakila.Repository.IRepository;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ApiSakila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly SakilaDbContext _dbContext;
        private readonly ILogger<LanguageController> _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly ILanguageRepository _languageRepo;

        public LanguageController(SakilaDbContext context, ILogger<LanguageController> logger, IMapper mapper, ILanguageRepository languageRepo)
        {
            _dbContext = context;
            _logger = logger;
            _mapper = mapper;
            _languageRepo = languageRepo;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetLanguages()
        {
            try
            {
                _logger.LogInformation("Get languages");

                IEnumerable<Language> languageList = await _languageRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<LanguageDto>>(languageList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        // Realizar una consulta de prueba
        //        var primerRegistro = _dbContext.Languages.FirstOrDefault();

        //        if (primerRegistro != null)
        //        {
        //            return Ok(primerRegistro);
        //        }
        //        else
        //        {
        //            return NotFound("No se encontraron registros.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error: {ex.Message}");
        //    }
        //}
    }
}

