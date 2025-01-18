using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Mapper;

namespace TasksEvaluation.Core.DTOs
{
	public class StudentDto : BaseDto  , IMapFrom
    {
        
		public string FullName { get; set; }
		
		public string MobileNumber { get; set; }
		
		public string Email { get; set; }

        [Display(Name ="Profile Picture")]
        public IFormFile ProfilePicFile { get; set; }
        public string ProfilePic { get; set; }

		public int? GroupId { get; set; }
        public Group Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
