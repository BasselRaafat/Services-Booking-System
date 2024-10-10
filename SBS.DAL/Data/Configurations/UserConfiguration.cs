using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DAL.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(x => x.Id);
		builder.HasMany(u => u.Bookings)
			.WithOne(b => b.User)
			.HasForeignKey(b => b.UserID);
		builder.HasMany(u=>u.Reviews)
			.WithOne(r=>r.User)
			.HasForeignKey(r => r.UserId);
	}
}
