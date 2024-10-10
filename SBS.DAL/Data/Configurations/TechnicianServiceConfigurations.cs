using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DAL.Data.Configurations;

internal class TechnicianServiceConfigurations : IEntityTypeConfiguration<TechnicianService>
{
	public void Configure(EntityTypeBuilder<TechnicianService> builder)
	{
		builder.HasKey(ts => new {ts.ServiceId, ts.TechnicianId})
			.HasName("TechnicianService_PK");
		
		builder.HasOne(ts => ts.Technician)
			.WithMany(t => t.TechnicianService)
			.HasForeignKey(ts => ts.TechnicianId)
			.HasConstraintName("Technician_TechnicianService_FK");
		
		builder.HasOne(ts => ts.Service)
			.WithMany(s => s.TechnicianService)
			.HasForeignKey(ts => ts.ServiceId)
			.HasConstraintName("Service_TechnicianService_Fk");
	}
}
