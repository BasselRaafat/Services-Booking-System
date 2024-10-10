using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DAL.Data.Configurations;

internal class ServicesConfigurations : IEntityTypeConfiguration<Service>
{
	public void Configure(EntityTypeBuilder<Service> builder)
	{
		builder.HasOne(s => s.Category)
			.WithMany(c => c.Services)
			.HasForeignKey(s => s.CategoryId)
			.HasConstraintName("Category_FK");
		builder.HasMany(s => s.TechnicianService)
			.WithOne(ts => ts.Service)
			.HasForeignKey(ts => ts.ServiceId);

	}
}
