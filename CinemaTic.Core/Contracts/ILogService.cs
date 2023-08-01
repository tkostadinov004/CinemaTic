using CinemaTic.Data.Enums;

namespace CinemaTic.Core.Contracts
{
    public interface ILogService
    {
        Task LogActionAsync(UserActionType type, string message, params object[] attributes);
    }
}
