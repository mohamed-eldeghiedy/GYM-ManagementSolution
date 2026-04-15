using BLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionDetails(int sessionId);
        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
        bool CreateSession(UpdateSessionViewModel model);
        bool UpdateSession(int sessionId, UpdateSessionViewModel model);
        bool DeleteSession(int sessionId);
    }
}
