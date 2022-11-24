using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IntranetAPI.DBContext
{
    public partial class bazadoprojektu12Context : DbContext
    {
        public bazadoprojektu12Context()
        {
        }

        public bazadoprojektu12Context(DbContextOptions<bazadoprojektu12Context> options )
            : base(options)
        {
        }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Privilige> Priviliges { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VUsersWithPrivilige> VUsersWithPriviliges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=db4free.net;Database=bazadoprojektu12;Uid=superadmin123;Pwd=Admin123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Privilige>(entity =>
            {
                entity.ToTable("priviliges");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "Email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PriviligesId, "UsersToPriviliges_idx");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.Priviliges)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PriviligesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsersToPriviliges");
            });

            modelBuilder.Entity<VUsersWithPrivilige>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_UsersWithPriviliges");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.PriviligeName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
