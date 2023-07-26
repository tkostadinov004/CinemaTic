using Cinema.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Contracts
{
    public interface ILogService
    {
        Task LogActionAsync(UserActionType type, string message, params object[] attributes);
    }
}
