

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Silownia_WebApi.Models;

namespace Silownia_WebApi.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> op) : base(op) { }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Trainers> Trainers { get; set; }
        public DbSet<Trainings> Trainings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<UserMembership> UserMemberships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reservation>(eb =>
            {
                eb.HasOne(r => r.User)
                .WithMany(u => u.reservations)
                .HasForeignKey(r => r.UserId);

                eb.HasOne(r => r.Trainings)
                .WithMany(t => t.reservations)
                .HasForeignKey(r => r.TrainingId);

                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.ReservationDate).HasColumnType("date");


            });

           

            builder.Entity<Trainings>(eb =>
            {
                eb.HasOne(t => t.Trainer)
                .WithMany(tr => tr.trainings)
                .HasForeignKey(t => t.TrainerId);

                eb.Property(x => x.Name).HasColumnType("varchar(255)");
                eb.Property(x => x.Description).HasColumnType("varchar(255)");
                eb.Property(x => x.StartDate).HasColumnType("date");
                eb.Property(x => x.Duration).HasColumnType("int");

            });

            builder.Entity<Membership>(eb =>
            {
                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.Name).HasColumnType("varchar(255)");
                eb.Property(x => x.PricePerMonth).HasColumnType("decimal(10,2)");
            });

            builder.Entity<Trainers>(eb =>
            {
                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.Name).HasColumnType("varchar(255)");
                eb.Property(x => x.Bio).HasColumnType("varchar(255)");

            });

            builder.Entity<UserMembership>(eb =>
            {
                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.userId).HasColumnType("varchar(255)");
                eb.Property(x => x.MembershipId).HasColumnType("int");
            });


        }

    }
}
