﻿using Cinema.Core.Contracts.Common;
using Cinema.Core.Utilities;
using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.ViewModels.Actors;
using Cinema.ViewModels.Cinemas;
using Cinema.ViewModels.Contracts;
using Cinema.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace Cinema.Core.Contracts
{
    public interface IOwnersService
    {
        Task AddAsync(IViewModel item, string userEmail);
        Task<IEnumerable<CinemaListViewModel>> GetAllByUserAsync(string userEmail);
        Task<CinemaDetailsViewModel> GetByIdAsync(int? id);
        Task<OwnerStatisticsViewModel> GetStatisticsAsync(string userEmail);
        Task AddMovieToCinemas(MovieDetailsViewModel viewModel);
        Task EditCinema(IViewModel item);
        Task DeleteByIdAsync(int? id);
        Task<IEnumerable<CinemaListViewModel>> SearchAndFilterCinemasAsync(string searchText, string userEmail, string filterValue, string sortBy);
        Task<IEnumerable<MovieInfoCardViewModel>> SearchMoviesByCinema(string searchText, string cinemaId);
        Task<EditCinemaViewModel> GetEditViewModelByIdAsync(int cinemaId);
        Task<DeleteCinemaViewModel> PrepareDeleteViewModelAsync(int id);
        Task<CinemaPagePreviewViewModel> PreparePreviewViewModelAsync(string userEmail, string cinemaId);
        Task<CustomerCinemaPageViewModel> PrepareCinemaViewModelAsync(string userEmail, string cinemaId);
        Task<IEnumerable<CinemaMovieViewModel>> GetMoviesByDateAsync(string cinemaId, string date);
        Task<IEnumerable<CinemaListViewModel>> GetCinemasContainingMovieAsync(string movieId, string userEmail);
    }
}
