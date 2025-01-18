using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.Business;

namespace TaskEvaluation.InfraStructure.EntityConfigs
{
	public class StudentConfig : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.Property(s => s.FullName).IsRequired();
			builder.Property(S => S.MobileNumber).IsRequired();
			builder.Property(s => s.Email).IsRequired();

			// one Student has many Solutions
			builder.HasMany(st => st.Solutions)
				.WithOne(sol => sol.Student)
				.HasForeignKey(sol => sol.StudentId);

		}


	}
}
