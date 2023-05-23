using DomainLayer.DTOs.VoteFormDtos;
using DomainLayer.Entities.VoteFormEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Abstraction.VoteFormInterfaces;
using System.Security.Claims;

namespace PresentaionLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteFormController : ControllerBase
    {
        private readonly IVoteFormService voteFormService;

        public VoteFormController(IVoteFormService voteFormService)
        {
            this.voteFormService = voteFormService;
        }

        [HttpPost,Route("AddVoteForm")]
        public IActionResult AddVoteForm(VoteForm voteForm)
        {

            return Ok(voteFormService.AddVoteForm(voteForm));
        }
        [HttpGet,Route("GetFormStatus")]
        public IActionResult GetFormStatus(Guid FormId)
        {
           return Ok( voteFormService.GetFormStatus(FormId));
        }
        [HttpPost, Route("GetFormsForEmployee")]
        public IActionResult GetFormsForEmployee(GetEmployeeFormsQuery getEmployeeFormsQuery)
        {
            return Ok(voteFormService.GetAllFormsForEmployee(getEmployeeFormsQuery));
        }
        [HttpGet,Route("GetAnchoredForm")]
        public IActionResult GetAnchoredForm(Guid FormId)
        {
           return Ok( voteFormService.GetForm(FormId));
        }
        [HttpPost,Route("Vote")]
        public IActionResult Vote(VoteDto voteDto)
        {
            // We Can Get Current Logged In User From JWT Token In The Next Line But I'm Getting It From
            //Object So We Can Test Faster
           // Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var Result = voteFormService.Vote(voteDto);
            if (Result)
            {
                return Ok(new { status = "Voted" });
            }
            return Ok(new { status = "Unable To Vote You Might be Voted Before Or The Form Expired" });

        }

        [HttpPost,Route("AddFormSeperated")]
        public IActionResult AddFormSeprated(AddVoteFormDTO addVoteFormDTO)
        {
            
            return Ok(voteFormService.AddFormSperated(addVoteFormDTO));    
        }
        [HttpPost, Route("AddQuestionSeperated")]
        public IActionResult AddQuestionSeprated(AddQuestionDto addQuestionDto)
        {
            
            return Ok(voteFormService.AddQuestionSprated(addQuestionDto));
        }
        [HttpPost, Route("AddChoisSeperated")]
        public IActionResult AddChoisSeperated(AddChoisDto addChoisDto)
        {
            
            return Ok(voteFormService.AddChoiseSeperated(addChoisDto));
        }
        [HttpGet,Route("GetQuestionsByFormId")]
        public IActionResult GetQuestionsByFormId(Guid FormId)
        {
            return Ok(voteFormService.GetQuestionsForForm(FormId));
        }
        [HttpGet,Route("GetChoisesByQuestionId")]
        public IActionResult GetChoisesByQuestionId(Guid QuestionId)
        {
            return Ok(voteFormService.GetChoicesForQuestion(QuestionId));
        }
    }
}
