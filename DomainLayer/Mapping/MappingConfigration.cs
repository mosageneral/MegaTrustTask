using AutoMapper;
using DomainLayer.DTOs.UserDtos;
using DomainLayer.DTOs.VoteFormDtos;
using DomainLayer.Entities.UserEntity;
using DomainLayer.Entities.VoteFormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Mapping
{
    public class MappingConfigration : Profile
    {


        public MappingConfigration()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, AddUserDTO>(MemberList.Source).ReverseMap();
            CreateMap<UserRole, AddUserToRoleDto>(MemberList.Source).ReverseMap();
            CreateMap<Vote, VoteDto>(MemberList.Source).ReverseMap();
            CreateMap<VoteForm, AddVoteFormDTO>(MemberList.Source).ReverseMap();
            CreateMap<Choice, AddChoisDto>(MemberList.Source).ReverseMap();
            CreateMap<Question, AddQuestionDto>(MemberList.Source).ReverseMap();

        }
    }

}
