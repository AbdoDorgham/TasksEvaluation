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
	public class CourseConfig : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.Property(c => c.Title).IsRequired(); 
			builder.Property(c => c.IsCompleted).HasDefaultValue(false);


			// One Course has many Groups
			builder.HasMany(s => s.Groups)
				.WithOne(g => g.Course)
				.HasForeignKey(g => g.CourseId);
		}
	}
}
