using Microsoft.EntityFrameworkCore;

namespace CrisisApi.Models
{
    public partial class CrisisDbContext : DbContext
    {
        public CrisisDbContext()
        {
        }

        public CrisisDbContext(DbContextOptions<CrisisDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appuser> Appuser { get; set; }
        public virtual DbSet<Coordinatesinfo> Coordinatesinfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appuser>(entity =>
            {
                entity.ToTable("appuser");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Coordinatesinfo>(entity =>
            {
                entity.ToTable("coordinatesinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Addedtime)
                    .HasColumnName("addedtime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Appuserid).HasColumnName("appuserid");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.HasOne(d => d.Appuser)
                    .WithMany(p => p.Coordinatesinfo)
                    .HasForeignKey(d => d.Appuserid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__coordinat__appus__5535A963");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
