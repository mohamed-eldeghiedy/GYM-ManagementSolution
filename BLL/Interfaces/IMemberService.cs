using BLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMember();
        bool CreateMember(CreateMemberViewModel model);
        MemberViewModel? GetMemberDetails(int memberid);
        HealthRecordViewModel? GetMemberHealthRecord(int memberid);
        
        MemberToUpdateViewModel? GetMemberToUpdate(int memberid );
        bool UpdateMemberDetails(int memberid, MemberToUpdateViewModel memberToUpdatemodel);
        bool DeleteMember(int memberid);
         
    }
}
