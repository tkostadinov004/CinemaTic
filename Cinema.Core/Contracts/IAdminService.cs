using Cinema.Data.Enums;
using Cinema.Data.Models;
using Cinema.ViewModels.Admin;

namespace Cinema.Core.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<bool> DemoteUserAsync(string id);
        Task<bool> DeleteAccountAsync(string id);
        Task<bool> PromoteUserAsync(string id);
        Task<bool> UserExistsAsync(string id);
        Task<bool> CinemaExistsAsync(int? id);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<Data.Models.Cinema>> GetAllCinemasAsync();
        Task<IEnumerable<AdminAllCinemasViewModel>> SearchAndFilterCinemasAsync(string searchText, string filterValue, string sortBy);
        Task<IEnumerable<UserDetailsViewModel>> SearchAndFilterUsersAsync(string searchText, string filterValue);
        Task<UserDetailsViewModel> GetUserDetailsViewModelByIdAsync(string id);
        Task<AdminCinemaDetailsViewModel> GetCinemaDetailsViewModelByIdAsync(int? id);
        Task<ChangeCinemaApprovalStatusViewModel> GetChangeApprovalStatusViewModelByIdAsync(int? id);
        Task ChangeApprovalByIdStatusAsync(int? id, ApprovalStatus approvalCode);
        Task<AdminUserCRUDViewModel> GetAdminUserCRUDPartialAsync(string id);
    }
}
