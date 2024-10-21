using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DAL.Data.Configurations;

internal class BookingConfigurations : IEntityTypeConfiguration<Booking>
{
	public void Configure(EntityTypeBuilder<Booking> builder)
	{
		builder.HasOne(b => b.User)
			.WithMany(u => u.Bookings)
			.HasForeignKey(b => b.UserID)
			.HasConstraintName("Booking_User_FK");

		builder.HasOne(b => b.Technician)
			.WithMany(t => t.Bookings)
			.HasForeignKey(b => b.TechnicianID)
			.HasConstraintName("Booking_Technician_FK");
        builder.Property(b => b.TotalPrice)
                .HasColumnType("decimal(4,2)");
    }
}
