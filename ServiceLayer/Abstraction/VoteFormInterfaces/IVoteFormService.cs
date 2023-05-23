using DomainLayer.DTOs.VoteFormDtos;
using DomainLayer.Entities.BaseEntity;
using DomainLayer.Entities.VoteFormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Abstraction.VoteFormInterfaces
{
    public interface IVoteFormService
    {
        public VoteForm AddVoteForm(VoteForm entity);
        public VoteFormStatusDto GetFormStatus(Guid FormId);
        public IEnumerable<GetFormsFormployeeResultDto> GetAllFormsForEmployee(GetEmployeeFormsQuery getEmployeeFormsQuery);
        public VoteForm GetForm(Guid FormId);
        public bool Vote(VoteDto voteDto);
        public BaseResult<VoteForm> AddFormSperated(AddVoteFormDTO voteFormDTO);
        public Question AddQuestionSprated(AddQuestionDto addQuestionDto);
        public Choice AddChoiseSeperated(AddChoisDto addChoisDto);
        public IEnumerable<Question> GetQuestionsForForm(Guid FormId);
        public IEnumerable<Choice> GetChoicesForQuestion(Guid QuestionId);
        public bool CheckEmployeeVoted(VoteForm voteForm, Guid EmployeeId);
    }
}
