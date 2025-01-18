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
	public class GroupConfig : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.Property(g => g.Title).IsRequired();

			// one Group has many students
			builder.HasMany(g => g.Students)
				.WithOne(s => s.Group)
				.HasForeignKey(s => s.GroupId);

			// one Group has many Assignments
			builder.HasMany(g => g.Assignments)
				.WithOne(a => a.Group)
				.HasForeignKey(a => a.GroupId);
		}


	}
}
