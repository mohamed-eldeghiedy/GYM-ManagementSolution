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
        bool CreateSession(CreateSessionViewModel session);
        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);        
        bool UpdateSession(int sessionId, UpdateSessionViewModel model);
        bool DeleteSession(int sessionId);
    }
}
