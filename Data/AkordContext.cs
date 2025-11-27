using Microsoft.EntityFrameworkCore;
using AkordideApi.Models;

namespace AkordideApi.Data
{
    public class AkordContext : DbContext
    {
        public AkordContext(DbContextOptions<AkordContext> options) : base(options) { }

        public DbSet<Kolmkola> Kolmkolad { get; set; } = null!;
        public DbSet<Takt> Taktid { get; set; } = null!;
        public DbSet<Lugu> Lood { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✔️ Наследование Kolmkola (TPH)
            modelBuilder.Entity<Kolmkola>()
                .HasDiscriminator<string>("Tyypp")
                .HasValue<Kolmkola>("Kolmkola")
                .HasValue<CKolmkola>("C")
                .HasValue<FKolmkola>("F")
                .HasValue<GKolmkola>("G");

            // ✔️ Связь Takt → Kolmkola
            modelBuilder.Entity<Takt>()
                .HasOne(t => t.Kolmkola)
                .WithMany(k => k.Taktid)
                .HasForeignKey(t => t.KolmkolaId)
                .OnDelete(DeleteBehavior.Cascade);   // <--- теперь можно удалять Kolmkola

            // ✔️ Связь Lugu → Takt
            modelBuilder.Entity<Lugu>()
                .HasMany(l => l.Taktid)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
