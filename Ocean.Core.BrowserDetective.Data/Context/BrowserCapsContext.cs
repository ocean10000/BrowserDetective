using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Ocean.Core.BrowserDetective.Data.Context;

public partial class BrowserCapsContext : DbContext
{
    public BrowserCapsContext()
    {
    }
    public BrowserCapsContext(string ConnectionString)
    {
        if (string.IsNullOrEmpty(ConnectionString) == false)
            this.Database.SetConnectionString(ConnectionString);
    }

    public BrowserCapsContext(DbContextOptions<BrowserCapsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Browser> Browsers { get; set; }

    public virtual DbSet<Models.Capability> Capabilities { get; set; }

    public virtual DbSet<Models.Capture> Captures { get; set; }

    public virtual DbSet<Models.Identification> Identifications { get; set; }

    public virtual DbSet<Models.SampleHeader> SampleHeaders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (ConfigurationManager.ConnectionStrings["BrowserCaps"] == null || string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings["BrowserCaps"].ConnectionString))
        {
            System.IO.FileInfo file = new FileInfo("BrowserCaps.DB");
            if (file.Exists)
                optionsBuilder.UseSqlite("DataSource=" + file.FullName);
            else
                optionsBuilder.UseSqlite();
        }
        else
        {
            optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["BrowserCaps"].ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Browser>(entity =>
        {
            entity.ToTable("Browsers");
            entity.HasIndex(e => e.Id, "IX_Browsers_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ParentId).HasColumnName("Parent_ID");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasForeignKey(d => d.ParentId);
        });

        modelBuilder.Entity<Models.Capability>(entity =>
        {
            entity.ToTable("Capabilities");
            entity.HasIndex(e => e.Id, "IX_Capabilities_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BrowserId).HasColumnName("Browser_ID");

            entity.HasOne(d => d.Browser).WithMany(p => p.Capabilities)
                .HasForeignKey(d => d.BrowserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Models.Capture>(entity =>
        {
            entity.ToTable("Capture");

            entity.HasIndex(e => e.Id, "IX_Capture_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BrowserId).HasColumnName("Browser_ID");

            entity.HasOne(d => d.Browser).WithMany(p => p.Captures)
                .HasForeignKey(d => d.BrowserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Models.Identification>(entity =>
        {
            entity.ToTable("Identification");

            entity.HasIndex(e => e.Id, "IX_Identification_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BrowserId).HasColumnName("Browser_ID");

            entity.HasOne(d => d.Browser).WithMany(p => p.Identifications)
                .HasForeignKey(d => d.BrowserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Models.SampleHeader>(entity =>
        {
            entity.ToTable("SampleHeader");

            entity.HasIndex(e => e.Id, "IX_SampleHeader_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BrowserId).HasColumnName("Browser_ID");
            entity.Property(e => e.Name).HasColumnName("Name");
            entity.Property(e => e.Value).HasColumnName("Value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
