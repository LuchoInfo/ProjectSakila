using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSakila.Models;
using ApiSakila.Models.Dto;
using ApiSakila.Repository.IRepository;
using AutoMapper;
using System.Net;

namespace ApiSakila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        //no cambiar tanto mi codgio hacer un commit. 
        private readonly ILogger<LanguageController> _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly IFilmRepository _filmRepository;
        private readonly ILanguageRepository _languageRepository;

        public FilmController(ILogger<LanguageController> logger, IMapper mapper, IFilmRepository filmRepository, ILanguageRepository languageRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _filmRepository = filmRepository;
            _languageRepository = languageRepository;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetFilms()
        {
            try
            {
                _logger.LogInformation("Get films");

                IEnumerable<Film> filmList = await _filmRepository.GetAll();
                _response.Result = _mapper.Map<IEnumerable<FilmDto>>(filmList);
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

        [HttpGet("{id:int}", Name = "GetFilm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetFilm(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el Film con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var film = await _filmRepository.Get(v => v.FilmId == id);

                if (film == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsSuccessful = false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<FilmDto>(film);
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateFilm([FromBody] FilmCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _filmRepository.Get(v => v.Title.ToLower() == createDto.Title.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "La Pelicula con ese Nombre ya existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Film modelo = _mapper.Map<Film>(createDto);

                modelo.LastUpdate = DateTime.Now;
                modelo.OriginalLanguage = null;
                modelo.Length = null;

                await _filmRepository.Create(modelo);

                FilmDto filmDto = _mapper.Map<FilmDto>(modelo);
                _response.Result = filmDto;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetFilm", new { id = modelo.FilmId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _filmRepository.Get(v => v.FilmId == id);
                if (villa == null)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _filmRepository.Delete(villa);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] FilmUpdateDto updateDto)
        {
            //Validations
            if (updateDto == null || id != updateDto.FilmId)
            {
                _response.IsSuccessful = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            //Validacion para "OriginalLanguageId" y "LanguageId" para que pertenezca a un padre
            var languageExists = await _languageRepository.Get(v => v.LanguageId == updateDto.LanguageId) != null;
            if (!languageExists)
            {
                ModelState.AddModelError("LanguageId", "El ID de idioma no existe en la tabla de idiomas.");
                return BadRequest(ModelState);
            }

            // Verifica si el usuario ingreso un OriginalLanguage y existe en la tabla Language
            if (updateDto.OriginalLanguageId.HasValue)
            {
                var originalLanguageExists = await _languageRepository.Get(v => v.LanguageId == updateDto.OriginalLanguageId.Value) != null;
                if (!originalLanguageExists)
                {
                    ModelState.AddModelError("OriginalLanguageId", "El ID de idioma original no existe en la tabla de idiomas.");
                    return BadRequest(ModelState);
                }
            }

            Film modelo = _mapper.Map<Film>(updateDto);
            modelo.LastUpdate = DateTime.Now;
            //modelo.OriginalLanguage = null; Verificar este funcionamiento ingresar sin ningun id en original language luego con id
            modelo.Length = null;

            await _filmRepository.Update(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);

        }
    }
}

