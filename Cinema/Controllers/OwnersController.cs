using Cinema.Core.Contracts;
using Cinema.Data.Models;
using Cinema.Utilities;
using Cinema.ViewModels.Cinemas;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class OwnersController : Controller
    {
        private IOwnersService _ownersService;

        public OwnersController(IOwnersService ownersService)
        {
            _ownersService = ownersService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _ownersService.GetStatisticsAsync(User.Identity.Name));
        }
        public async Task<IActionResult> UserCinemas()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddCinema()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCinema(AddCinemaViewModel viewModel, string userEmail)
        {
            if (ModelState.IsValid)
            {
                await _ownersService.AddAsync(viewModel, userEmail);
            }
            return RedirectToAction("Index", "UserCinemas");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            return View(await _ownersService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddMovieToCinemas([FromForm] string[] cinemas, int movieId)
        {
            await _ownersService.AddMovieToCinemas(cinemas, movieId);
            return RedirectToAction("Index", "Owners");
        }
        [HttpGet]
        public async Task<IActionResult> EditCinema(string cinemaId)
        {
            return PartialView("_EditCinemaPartial", await _ownersService.GetEditViewModelByIdAsync(int.Parse(cinemaId)));
        }
        [HttpPost]
        public async Task<IActionResult> EditCinema([FromForm] EditCinemaViewModel viewModel, int cinemaId)
        {
            viewModel.Id = cinemaId;
            await _ownersService.EditCinema(viewModel);
            return RedirectToAction("UserCinemas", "Owners");
        }
        public async Task<IActionResult> DeleteCinema(int? id)
        {
            await _ownersService.DeleteByIdAsync(id);
            return RedirectToAction("UserCinemas", "Owners");
        }

        public async Task<IActionResult> SearchAndFilterCinemas(string searchText, string userEmail, string filterValue)
        {
            var cinemas = await _ownersService.SearchAndFilterCinemasAsync(searchText, userEmail, filterValue);
            return PartialView("_CinemasPartial", cinemas);
        }
        public async Task<IActionResult> SearchMoviesByCinema(string searchText, string id)
        {
            var movies = await _ownersService.SearchMoviesByCinema(searchText, id);
            return PartialView("_CinemaMoviesPartial", movies);
        }
    }
}
