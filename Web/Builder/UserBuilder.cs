﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Db.Entity;

namespace Web.Builder
{
    public class UserBuilder : User
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.email).IsRequired().HasMaxLength(255);
            builder.Property(user => user.password).IsRequired().HasMaxLength(150);



            builder.HasMany(user => user.Roadmaps)
                .WithOne(roadmap => roadmap.Users)
                .HasForeignKey(roadmap => roadmap.UserId)
                .OnDelete(DeleteBehavior.Cascade) 
                .IsRequired();
        }
    }
}
