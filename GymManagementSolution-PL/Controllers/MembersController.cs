using BLL.Interfaces;
using BLL.ViewModels.MemberViewModels;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSolution_PL.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = _memberService.GetAllMember();
            return View(members);
        }
        public IActionResult MemberDetails(int id)
        {
            if (id <= 0)
            { 
                TempData["ErrorMessage"] = "Invalid member ID.";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberDetails(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public IActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid member ID.";
                return RedirectToAction(nameof(Index));
            }
            var memberHealthRecord = _memberService.GetMemberHealthRecord(id);
            if (memberHealthRecord == null)
            {
                TempData["ErrorMessage"] = "Member health record not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(memberHealthRecord);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel createModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors in the form.");
                return View(nameof(Create), createModel);
            }
            bool result = _memberService.CreateMember(createModel);
            if (!result)
            {
                TempData["ErrorMessage"] = "Failed to create member.";
                
            }
            else
            {
                TempData["SuccessMessage"] = "Member created successfully.";
                
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult MemberEdit(int id )
        {
            if( id<= 0 )
            {
                TempData["ErrorMessage"] = "Id of Member can not be 0 or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberToUpdate(id);
            if(member is null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        [HttpPost]
        public IActionResult MemberEdit([FromRoute]int id , MemberToUpdateViewModel memberToUpdate) 
        {
            if (!ModelState.IsValid)
                return View(memberToUpdate);
            bool result = _memberService.UpdateMemberDetails(id, memberToUpdate);
            if (!result)
                TempData["ErrorMessage"] = "Failed to update member.";
            else
                TempData["SuccessMessage"] = "Member updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult MemberDelete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid member ID.";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberDetails(id); 
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public IActionResult MemberDeleteConfirmed([FromRoute]int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid member ID.";
                return RedirectToAction(nameof(Index));
            }
            bool result = _memberService.DeleteMember(id);
            if (!result)
            {
                TempData["ErrorMessage"] = "Failed to delete member.";
            }
            else
            {
                TempData["SuccessMessage"] = "Member deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
