using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Ocean.Core.BrowserDetective.Data.Context;

public partial class BrowserCapsContext : DbContext
{
    private string Conn = string.Empty;
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    IConfiguration? configuration = null;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public BrowserCapsContext()
    {
    }
    public BrowserCapsContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public BrowserCapsContext(string ConnectionString)
    {
        if (string.IsNullOrEmpty(ConnectionString) == false)
            Conn = ConnectionString;
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
        if (string.IsNullOrEmpty(this.Conn) == false)
        {
            optionsBuilder.UseSqlite(Conn);
            return;
        }
        else if (configuration != null && string.IsNullOrWhiteSpace(configuration?.GetSection("ConnectionStrings")["BrowserCaps"]) == false)
        {
            Conn = configuration.GetSection("ConnectionStrings")["BrowserCaps"];
            optionsBuilder.UseSqlite(Conn);
            return;
        }
        else if (System.Configuration.ConfigurationManager.ConnectionStrings["BrowserCaps"] != null && string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.ConnectionStrings["BrowserCaps"].ConnectionString) == false)
        {
            Conn = System.Configuration.ConfigurationManager.ConnectionStrings["BrowserCaps"].ConnectionString;
            optionsBuilder.UseSqlite(Conn);
            return;
        }
        else
        {
            //Last Ditch Attempts at finding the BrowserCaps.DB File.
            var list = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "BrowserCaps.DB", SearchOption.AllDirectories).ToList();

            if (list.Count > 0)
            {
                var file = new FileInfo(list[0]);
                if (file.Exists)
                {
                    optionsBuilder.UseSqlite("DataSource=" + file.FullName);
                    return;
                }
            }

            //pulls the copy embeded in the dll, and copy it to the folder.
            if (System.IO.File.Exists("BrowserCaps.DB") == false)
            {
                string Resources = "Ocean.Core.BrowserDetective.BrowserCaps.DB";
                var a = this.GetType().Assembly;
                var stream = a.GetManifestResourceStream(Resources);

                using (System.IO.FileStream w = new FileStream("BrowserCaps.DB", FileMode.OpenOrCreate))
                {
                    stream.CopyTo(w);
                    w.Flush();
                    w.Close();
                    optionsBuilder.UseSqlite("DataSource=BrowserCaps.DB");
                }
            }

            optionsBuilder.UseSqlite();
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
