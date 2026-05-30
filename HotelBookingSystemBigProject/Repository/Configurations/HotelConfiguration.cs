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
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.Property(h => h.Name)
                   .IsRequired()
                   .HasMaxLength(100);


            builder.Property(h => h.Description)
                   .HasMaxLength(100);

            builder.Property(h => h.ContactInformation)
                   .HasMaxLength(100);



            builder.HasMany(h => h.Rooms)
                   .WithOne(h => h.Hotel)
                   .HasForeignKey(h => h.HotelID);
        }
    }
}
