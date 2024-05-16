using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebSakila.Models;
using WebSakila.Models.Dto;
using WebSakila.Services.IServices;

namespace WebSakila.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public FilmController(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexFilm()
        {

            List<FilmDto> filmList = new();

            var response = await _filmService.GetAll<APIResponse>();

            if (response != null && response.IsSuccessful)
            {
                filmList = JsonConvert.DeserializeObject<List<FilmDto>>(Convert.ToString(response.Result));
            }

            return View(filmList);
        }

        //Get
        public async Task<IActionResult> CreateFilm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFilm(FilmCreateDto modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _filmService.Create<APIResponse>(modelo);

                if (response != null && response.IsSuccessful)
                {

                    TempData["exitoso"] = "Film Create successfully";
                    return RedirectToAction(nameof(IndexFilm));
                }
            }
            return View(modelo);
        }


        public async Task<IActionResult> UpdateFilm(int filmId)
        {
            var response = await _filmService.Get<APIResponse>(filmId);

            if (response != null && response.IsSuccessful)
            {
                FilmDto model = JsonConvert.DeserializeObject<FilmDto>(Convert.ToString(response.Result));
                return View(_mapper.Map<FilmUpdateDto>(model));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFilm(FilmUpdateDto modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _filmService.Update<APIResponse>(modelo);

                if (response != null && response.IsSuccessful)
                {
                    TempData["exitoso"] = "Film Update successfully";
                    return RedirectToAction(nameof(IndexFilm));
                }
            }
            return View(modelo);
        }


        public async Task<IActionResult> DeleteFilm(int filmId)
        {
            var response = await _filmService.Get<APIResponse>(filmId);

            if (response != null && response.IsSuccessful)
            {
                FilmDto model = JsonConvert.DeserializeObject<FilmDto>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFilm(FilmDto modelo)
        {

            var response = await _filmService.Delete<APIResponse>(modelo.FilmId);

            if (response != null && response.IsSuccessful)
            {
                TempData["exitoso"] = "Film Delete Successfully";
                return RedirectToAction(nameof(IndexFilm));
            }
            TempData["error"] = "An error occurred while removing";
            return View(modelo);
        }


    }
}