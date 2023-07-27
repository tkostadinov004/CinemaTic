using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.ViewModels.Admin;

namespace Cinema.Core.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task DemoteUser(string id);
        Task DeleteAccount(string id);
        Task PromoteUser(string id);
        Task<bool> UserExistsAsync(string id);
        Task<bool> CinemaExistsAsync(int id);
        Task<ApplicationUser> FindById(string id);
        Task<IEnumerable<Data.Models.Cinema>> GetAllCinemasAsync();
        Task<IEnumerable<AdminAllCinemasViewModel>> SearchAndFilterCinemasAsync(string searchText, string filterValue, string sortBy);
        Task<IEnumerable<UserDetailsViewModel>> SearchAndFilterUsersAsync(string searchText, string filterValue);
        Task<UserDetailsViewModel> GetUserDetailsAsync(string id);
        Task<AdminCinemaDetailsViewModel> GetCinemaDetailsAsync(int? id);
        Task<ChangeCinemaApprovalStatusViewModel> GetCASViewModelAsync(int id);
        Task ChangeApprovalStatusAsync(int id, ApprovalStatus approvalCode);
        Task<AdminUserCRUDViewModel> GetAdminUserCRUDPartialAsync(string id);
    }
}
