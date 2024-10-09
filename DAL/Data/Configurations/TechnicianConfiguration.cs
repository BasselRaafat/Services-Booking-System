using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DAL.Data.Configurations;

internal class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
{
	public void Configure(EntityTypeBuilder<Technician> builder)
	{
		builder.HasMany(t => t.Reviews)
			.WithOne(r => r.Technician)
			.HasForeignKey(r => r.TechnicianId);
		builder.HasMany(t => t.Bookings)
			.WithOne(b => b.Technician)
			.HasForeignKey(b=>b.TechnicianID);
		builder.HasMany(t => t.TechnicianService)
			.WithOne(ts => ts.Technician)
			.HasForeignKey(ts => ts.ServiceId)
			.HasConstraintName("Technician_Service_FK");
	}
}
