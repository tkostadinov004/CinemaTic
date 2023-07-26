﻿using Cinema.Core.Contracts;
using Cinema.Core.Services;
using Cinema.Core.Utilities;
using Cinema.ViewModels.Customers;
using Cinema.ViewModels.Sectors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly ISectorsService _sectorsService;
        private readonly IMoviesService _moviesService;

        public CustomerController(ICustomersService customersService, ISectorsService sectorsService, IMoviesService moviesService)
        {
            _customersService = customersService;
            _sectorsService = sectorsService;
            _moviesService = moviesService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _customersService.GetCinemasForUserAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetTicketPurchaseView(string cinemaId)
        {
            return PartialView("_CinemaSectorsGridPartial", await _sectorsService.GetCinemaSectorsGridAsync(cinemaId, null, new System.DateTime()));
        }
        public async Task<IActionResult> MovieDetails(int id)
        {
            return View("MovieDetails", await _moviesService.GetDetailsViewModel(await _moviesService.GetByIdAsync(id), null, User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View("ChangePassword", await _customersService.GetChangePasswordViewModelAsync(User.Identity.Name));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePassword", await _customersService.GetChangePasswordViewModelAsync(User.Identity.Name));
            }
            await _customersService.ChangePasswordAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Cinemas(bool? all, int? pageNumber)
        {
            TempData["All"] = all;
            var cinemas = await _customersService.GetCinemasAsync(all, User.Identity.Name);
            return View("Cinemas", await PaginatedList<CinemasViewModel>.CreateAsync(cinemas, pageNumber ?? 1, 5));
        }
        public async Task<IActionResult> AddCinemaToFavorites(int id)
        {
            await _customersService.AddCinemaToFavoritesAsync(id, User.Identity.Name);
            return RedirectToAction("Cinemas", new { all = false });
        }
        public async Task<IActionResult> RemoveCinemaFromFavorites(int id)
        {
            await _customersService.RemoveCinemaFromFavoritesAsync(id, User.Identity.Name);
            return RedirectToAction("Cinemas", new { all = false });
        }
        public async Task<IActionResult> Tickets(int? pageNumber)
        {
            var tickets = await _customersService.GetTicketsForCustomerAsync(User.Identity.Name);
            return View("Tickets", await PaginatedList<CustomerTicketViewModel>.CreateAsync(tickets, pageNumber ?? 1, 5));
        }
        public async Task<IActionResult> Cinema(string cinemaId)
        {
            return View("Cinema", await _customersService.PrepareCinemaViewModelAsync(User.Identity.Name, cinemaId));
        }
        public async Task<IActionResult> BuyTicket(int cinemaId, int movieId, string forDate)
        {
            return View("BuyTicket", await _customersService.GetBuyTicketViewModelAsync(cinemaId, movieId, forDate));
        }
        [HttpPost]
        public async Task<IActionResult> BuyTicket(int sectorId, int movieId, SectorDetailsViewModel viewModel, DateTime forDate)
        {
            await _customersService.BuyTicketAsync(sectorId, movieId, viewModel, forDate, User.Identity.Name);
            return RedirectToAction("Cinema", "Customer", new { userEmail = User.Identity.Name, cinemaId = TempData["CinemaId"] });
        }
        [HttpGet]
        public async Task<IActionResult> GetMoviesByDate(string cinemaId, string date)
        {
            return PartialView("_MoviesByDatePartial", await _customersService.GetMoviesByDateAsync(cinemaId, date));
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> SetRating(int movieId, int? rating)
        {
            if (rating == 0 || rating == null)
            {
                return RedirectToAction("MovieDetails", "Customer", new { id = movieId });
            }

            await _customersService.SetRatingAsync(movieId, rating.Value, User.Identity.Name);

            return RedirectToAction("MovieDetails", "Customer", new { id = movieId });
        }
    }
}
