﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("IdUsuario");

            builder.Property(e => e.FirstName)
                .HasColumnName("Nombres")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .HasColumnName("Apellidos")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.DateOfBirth)
               .HasColumnName("FechaNacimiento")
               .HasColumnType("date");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Telephone)
                .HasColumnName("Telefono")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
               .HasColumnName("Activo");

            builder.Property(e => e.Password)
             .HasColumnName("Contrasena")
             .IsRequired()
             .HasMaxLength(200)
             .IsUnicode(false);

            builder.Property(e => e.Role)
              .HasColumnName("Rol")
              .IsRequired()
              .HasMaxLength(15)
              .HasConversion(
              x => x.ToString(),
              x => (RoleType)Enum.Parse(typeof(RoleType), x)
              );
        }
    }
}
