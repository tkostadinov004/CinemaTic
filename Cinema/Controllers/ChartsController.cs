﻿using Cinema.Core.Contracts;
using Cinema.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class ChartsController : Controller
    {
        private readonly IChartsService _chartsService;

        public ChartsController(IChartsService chartsService)
        {
            _chartsService = chartsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMarketShare()
        {
            return Json(await _chartsService.GetMarketShareByUserAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetTotalIncomes()
        {
            return Json(await _chartsService.GetTotalIncomesAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersPerCinema()
        {
            return Json(await _chartsService.GetCustomersPerCinemaAsync(User.Identity.Name));
        }
        [HttpGet]
        public async Task<IActionResult> GetBestSellingMoviesPerCinema()
        {
            return Json(await _chartsService.GetBestSellingMoviesPerCinemaAsync(User.Identity.Name));
        }
    }
}
