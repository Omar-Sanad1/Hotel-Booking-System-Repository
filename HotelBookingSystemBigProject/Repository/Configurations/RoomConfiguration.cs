using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasMany(r => r.Reviews)
                   .WithOne(r => r.Room)
                   .HasForeignKey(r => r.RoomID);

            builder.HasMany(r => r.Reservations)
                   .WithOne(r => r.Room)
                   .HasForeignKey(r => r.RoomID);


        }
    }
}
