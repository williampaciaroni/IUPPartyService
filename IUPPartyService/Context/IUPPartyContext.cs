using System;
using IUPPartyService.Models;
using Microsoft.EntityFrameworkCore;

namespace IUPPartyService.Context
{
    public class IUPPartyContext : DbContext
    {

        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participant { get; set; }

        public IUPPartyContext()
        {
        }

        public IUPPartyContext(DbContextOptions<IUPPartyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Event>()
                    .Property(e => e.EventID)
                    .ValueGeneratedOnAdd();

            //OneToMany Event and Participant
            modelBuilder.Entity<Participant>()
                    .HasOne(p => p.Event)
                    .WithMany(e => e.Participant)
                    .HasForeignKey(p => p.EventRef);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
    }
}
