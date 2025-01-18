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
	public class SolutionConfig : IEntityTypeConfiguration<Solution>
	{
		public void Configure(EntityTypeBuilder<Solution> builder)
		{
			builder.Property(s => s.SolutionFile).IsRequired();

			// one Solution has one Assignment
			builder.HasOne(s => s.Assignment)
				.WithMany(a => a.Solutions)
				.HasForeignKey(s => s.AssignmentId);
			
		}
	}
}
