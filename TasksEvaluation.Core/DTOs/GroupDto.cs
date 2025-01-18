using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Mapper;

namespace TasksEvaluation.Core.DTOs
{
	public class GroupDto : BaseDto ,IMapFrom
    {
		public string Title { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Group, GroupDto>().ReverseMap();
        }
    }
}
