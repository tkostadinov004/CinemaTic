using Cinema.Data.Enums;

namespace Cinema.Core.Contracts
{
    public interface ILogService
    {
        Task LogActionAsync(UserActionType type, string message, params object[] attributes);
    }
}
