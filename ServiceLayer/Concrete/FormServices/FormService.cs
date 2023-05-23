using AutoMapper;
using BusinessLayer.Abstraction;
using DomainLayer.DTOs.VoteFormDtos;
using DomainLayer.Entities.BaseEntity;
using DomainLayer.Entities.UserEntity;
using DomainLayer.Entities.VoteFormEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Server;
using PagedList;
using ServiceLayer.Abstraction.VoteFormInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Concrete.FormServices
{
    public class VoteFormService : IVoteFormService
    {
        private readonly IMapper mapper;
        private readonly IRepository<VoteForm> voteFormRepo;
        private readonly IRepository<Question> questionRepo;
        private readonly IRepository<Choice> choiceRepo;
        private readonly IRepository<Vote> voteRepo;
        private readonly IRepository<VoteFormEmployee> voteFormEmployeeRepo;
        private readonly IRepository<User> userRepo;

        public VoteFormService(IMapper Mapper,
            IRepository<VoteForm> VoteFormRepo,
            IRepository<Question> QuestionRepo,
            IRepository<Choice> ChoiceRepo,
            IRepository<Vote> VoteRepo,
            IRepository<VoteFormEmployee> VoteFormEmployeeRepo,
            IRepository<User> UserRepo
         
            )
        {
            mapper = Mapper;
            voteFormRepo = VoteFormRepo;
            questionRepo = QuestionRepo;
            choiceRepo = ChoiceRepo;
            voteRepo = VoteRepo;
            voteFormEmployeeRepo = VoteFormEmployeeRepo;
            userRepo = UserRepo;
        }
        public VoteForm AddVoteForm(VoteForm entity)
        {
           voteFormRepo.Add(entity);
            voteFormRepo.Save();

            return entity;
        }

        public IEnumerable<GetFormsFormployeeResultDto> GetAllFormsForEmployee(GetEmployeeFormsQuery getEmployeeFormsQuery)
        {
          var getFormsForEmployeeResultDtoList = new List<GetFormsFormployeeResultDto>();
             var AllFormsForUser = voteFormEmployeeRepo.GetMany(a => a.UserId == getEmployeeFormsQuery.EmployeeId).Include(a=>a.VoteForm).ThenInclude(a=>a.Questions).ToList().Select(a=>a.VoteForm).ToPagedList(getEmployeeFormsQuery.page,getEmployeeFormsQuery.PageCount);
            if (!string.IsNullOrEmpty(getEmployeeFormsQuery.FormTitle))
            {
                AllFormsForUser.Where(a => a.Title.Contains(getEmployeeFormsQuery.FormTitle));
            }
            foreach (var item in AllFormsForUser)
            {
                GetFormsFormployeeResultDto getFormsForEmployeeResultDto = new GetFormsFormployeeResultDto();
                getFormsForEmployeeResultDto.IsVoted = CheckEmployeeVoted(item, getEmployeeFormsQuery.EmployeeId);
                getFormsForEmployeeResultDto.VoteForm = item;
                getFormsForEmployeeResultDto.VoteForm.Questions = null;
                getFormsForEmployeeResultDtoList.Add(getFormsForEmployeeResultDto);
            }
            return getFormsForEmployeeResultDtoList;
        }

        public VoteForm GetForm(Guid FormId)
        {
            var anchoredForm= voteFormRepo.GetMany(a=>a.Id==FormId).Include(a => a.Questions).ThenInclude(a => a.Choices).FirstOrDefault();
            return anchoredForm;
        }
        public bool Vote(VoteDto voteDto)
        {
            var IsVotedBefore = voteRepo.GetMany(a => a.UserId == voteDto.UserId && a.QuestionId == voteDto.QuestionId).FirstOrDefault();
            if (IsVotedBefore != null)
            {
                return false;
            }
            var VotingForm = voteFormRepo.GetMany(a => a.Questions.Any(a => a.Id == voteDto.QuestionId)).FirstOrDefault();
            if (DateTime.Compare(DateTime.Now,VotingForm.EndDate)>0)
            {
                return false;
            }
            var Vote = mapper.Map<Vote>(voteDto);

            voteRepo.Add(Vote);
            voteRepo.Save();

            var Choise = choiceRepo.GetById(voteDto.ChoiceId);
            if (Choise!=null)
            {
                Choise.Votes++;
                choiceRepo.Update(Choise);
                choiceRepo.Save();
            }
           
            return true;

        }
        public VoteFormStatusDto GetFormStatus(Guid FormId)
        {
            var VoteForm = voteFormRepo.GetMany(a=>a.Id==FormId).Include(a=>a.Questions).ThenInclude(a=>a.Choices).FirstOrDefault();
   
            var FormVoteCount = 0;

            List<VoteFormQuestionStatusDto> voteFormQuestionStatusDtoList = new List<VoteFormQuestionStatusDto>();
       
            foreach (var question in VoteForm.Questions)
            {
                var QuestionVotes = 0;
                VoteFormQuestionStatusDto voteFormQuestionStatusDto = new VoteFormQuestionStatusDto();
                voteFormQuestionStatusDto.Question=question;
                
                foreach (var choise in question.Choices)
                {
                    var ChoiceVotes = voteRepo.GetMany(a => a.ChoiceId==choise.Id).Count();
                    QuestionVotes += ChoiceVotes;
                }
                voteFormQuestionStatusDto.VoteCount = QuestionVotes;
                voteFormQuestionStatusDto.Question.VoteForm = null;
                voteFormQuestionStatusDtoList.Add(voteFormQuestionStatusDto);
                FormVoteCount += QuestionVotes;
            }
            VoteForm.Questions = null;

            VoteFormStatusDto voteFormStatusDto = new VoteFormStatusDto
            {
                VoteForm = VoteForm,
                FormVotesCount = FormVoteCount,
                VoteFormQuestionStatus = voteFormQuestionStatusDtoList
            };

            foreach (var item in voteFormStatusDto.VoteFormQuestionStatus)
            {
                item.Percentage = item.VoteCount / FormVoteCount * 100;
            }

            return voteFormStatusDto;
        }

        public BaseResult<VoteForm> AddFormSperated(AddVoteFormDTO voteFormDTO)
        {
            if (voteFormDTO.EmployesIds.Count<2)
            {
                return new BaseResult<VoteForm> { Erorr = "Employees Must Be More Than 1", Result = null, StatusCode = "400" };
            }
            var Form = mapper.Map<VoteForm>(voteFormDTO);
            voteFormRepo.Add(Form);
            voteFormRepo.Save();

            foreach (var Id in voteFormDTO.EmployesIds)
            {
                voteFormEmployeeRepo.Add(new VoteFormEmployee { UserId = Id, VoteFormId = Form.Id });
            }
            voteFormEmployeeRepo.Save();

            return new BaseResult<VoteForm> { Erorr = "", Result = Form, StatusCode = "200" };
        }

        public Question AddQuestionSprated(AddQuestionDto addQuestionDto)
        {
            var Question = mapper.Map<Question>(addQuestionDto);
            questionRepo.Add(Question);
            questionRepo.Save();
            return Question;
        }

        public Choice AddChoiseSeperated(AddChoisDto addChoisDto)
        {
            var Choice = mapper.Map<Choice>(addChoisDto);
            choiceRepo.Add(Choice);
            choiceRepo.Save();
            return Choice;

        }

        public IEnumerable<Question> GetQuestionsForForm(Guid FormId)
        {
            return questionRepo.GetMany(a => a.VoteFormId == FormId).ToHashSet();
        }

        public IEnumerable<Choice> GetChoicesForQuestion(Guid QuestionId)
        {
            return choiceRepo.GetMany(a => a.QuestionId == QuestionId).ToHashSet();

        }

        public bool CheckEmployeeVoted(VoteForm voteForm, Guid EmployeeId)
        {
            
            foreach (var item in voteForm.Questions)
            {
                var IsVoted = voteRepo.GetMany(a => a.UserId == EmployeeId&&a.QuestionId==item.Id);
                if (IsVoted!=null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
