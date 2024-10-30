using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ocean.Core.BrowserDetective.Data.Models;
using System.Configuration;

namespace Ocean.Core.BrowserDetective.Data.Context
{
    public partial class ResultContext : DbContext
    {
        string _Conn = string.Empty;
        public ResultContext()
        {
        }
        public ResultContext(string connString)
        {
            _Conn = connString;
            this.Database.SetConnectionString(connString);
        }

        public ResultContext(DbContextOptions<ResultContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(_Conn))
                optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["Results"].ConnectionString);
            else
                optionsBuilder.UseSqlite(_Conn);
        }

        public virtual DbSet<ResultItem> Results { get; set; }
        public virtual DbSet<BrowserResult> Result { get; set; }

        public virtual DbSet<BrowserNode> Nodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResultItem>(entity =>
            {
                entity.ToTable("Results");
                entity.HasIndex(e => e.ID, "IX_Result_ID").IsUnique();

                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.Raw_ID).HasColumnName("Raw_ID");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Value).HasColumnName("Value");

            });
            modelBuilder.Entity<Models.BrowserNode>(entity =>
            {
                entity.ToTable("BrowsersNodes");
                entity.HasIndex(e => e.ID, "IX_Node_ID").IsUnique();

                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.Raw_ID).HasColumnName("Raw_ID");
                entity.Property(e => e.Node_ID).HasColumnName("BrowserNode_ID");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Value).HasColumnName("Value");

            });
            modelBuilder.Entity<Models.BrowserResult>(entity =>
            {
                entity.ToTable("Result");
                entity.HasIndex(e => e.Raw_ID, "IX_Raw_ID").IsUnique();
                entity.Property(e => e.Raw_ID).HasColumnName("Raw_ID");
                entity.Property(e => e.UserAgent).HasColumnName("UserAgent");
                entity.Property(e => e.BrowserName).HasColumnName("BrowserName");
                entity.Property(e => e.Crawler).HasColumnName("Crawler");
                entity.Property(e => e.version).HasColumnName("version");
                entity.Property(e => e.isMobileDevice).HasColumnName("isMobileDevice");
                entity.Property(e => e.mobileDeviceModel).HasColumnName("mobileDeviceModel");
                entity.Property(e => e.OS).HasColumnName("OS");
                entity.Property(e => e.platform).HasColumnName("platform");
                entity.Property(e => e.layoutEngineVersion).HasColumnName("layoutEngineVersion");
                entity.Property(e => e.layoutEngine).HasColumnName("layoutEngine");
                entity.Property(e => e.appleWebTechnologyVersion).HasColumnName("appleWebTechnologyVersion");
                entity.Property(e => e.Chromeversion).HasColumnName("Chromeversion");
                entity.Property(e => e.Stamp).HasColumnName("Stamp");
                entity.Property(e => e.TimeSpent).HasColumnName("TimeSpent");
                entity.Property(e => e.MD5).HasColumnName("MD5");
            });
        }
    }
}
