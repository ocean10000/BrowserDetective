using Microsoft.EntityFrameworkCore;

namespace Ocean.Core.BrowserDetective.Console.DataContext
{
    public partial class ResultContext : DbContext
    {
        public ResultContext()
        {
        }

        public ResultContext(DbContextOptions<ResultContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite("Data Source=C:\\local\\Core.Results.db");

        public virtual DbSet<Models.ResultItem> Results { get; set; }

        public virtual DbSet<Models.BrowserNode> Nodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ResultItem>(entity =>
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
                entity.Property(e => e.Index).HasColumnName("Index");

            });
        }
    }
}
