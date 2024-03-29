﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MFiles.PeraniAndPartners.Backend.Models
{
    public partial class IntranetPeraniContext : DbContext
    {
        public IntranetPeraniContext()
        {
        }

        public IntranetPeraniContext(DbContextOptions<IntranetPeraniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Domain> vw_domainnames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_domainnames");

                entity.Property(e => e.Cliente).HasMaxLength(100);

                entity.Property(e => e.DataRegistrazione).HasColumnType("datetime");

                entity.Property(e => e.DataScadenza).HasColumnType("datetime");

                entity.Property(e => e.Estensione).HasMaxLength(10);

                entity.Property(e => e.NomeDominio).HasMaxLength(100);

                entity.Property(e => e.NomeDominioCompleto).HasMaxLength(100);

                entity.Property(e => e.NoteExRegistrar).HasColumnType("ntext");

                entity.Property(e => e.NoteGenerali).HasColumnType("ntext");

                entity.Property(e => e.Owner).HasMaxLength(100);

                entity.Property(e => e.Provider).HasMaxLength(100);

                entity.Property(e => e.Registrar).HasMaxLength(100);

               entity.Property(e => e.Stato).HasMaxLength(100);

               entity.Property(e => e.NumeroPratica).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}