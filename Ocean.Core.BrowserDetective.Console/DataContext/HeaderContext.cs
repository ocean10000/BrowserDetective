using Microsoft.EntityFrameworkCore;
using System.Linq;
using Ocean.Core.BrowserDetective.Models;

namespace Ocean.Core.BrowserDetective.Console.DataContext
{
    public partial class HeaderContext : DbContext
    {
        public HeaderContext()
        {
        }

        public HeaderContext(DbContextOptions<HeaderContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=G:\\My Drive\\Databases\\Headers.DB");
        public virtual DbSet<Header> Headers { get; set; }
        public virtual DbSet<HeaderRaw> Raw { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Header>(entity =>
            {
                entity.ToTable("Headers");
                entity.HasIndex(e => e.ID, "IX_Header_ID").IsUnique();

                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.Raw_ID).HasColumnName("Raw_ID");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Value).HasColumnName("Value");

                entity.HasOne(e => e.Raw).WithMany(e => e.Headers)
                .HasForeignKey(e => e.Raw_ID);
            });
            modelBuilder.Entity<HeaderRaw>(entity =>
            {
                entity.ToTable("RawHeadersData");
                entity.HasIndex(e => e.ID, "IX_RawHeadersData_ID").IsUnique();

                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.CheckSum).HasColumnName("CheckSum");
                entity.Property(e => e.CheckSumValues).HasColumnName("CheckSumValues");
                entity.Property(e => e.Raw).HasColumnName("Raw");
                entity.Property(e => e.Stamp).HasColumnName("Stamp");
                entity.Property(e => e.FileName).HasColumnName("FileName");
            });
        }
    }
}
