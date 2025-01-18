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
	public class AssignmentConfig : IEntityTypeConfiguration<Assignment>
	{
		public void Configure(EntityTypeBuilder<Assignment> builder)
		{
			builder.Property(a => a.Title).IsRequired();
			builder.Property(a => a.Description).HasMaxLength(250);
			

		}
	}
}
