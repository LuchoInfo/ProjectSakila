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
        private readonly ILogger<LanguageController> _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly ILanguageRepository _languageRepo;

        public LanguageController(ILogger<LanguageController> logger, IMapper mapper, ILanguageRepository languageRepo)
        {
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

    }
}

