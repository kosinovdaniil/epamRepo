namespace ORM
{
    using System.Data.Entity;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=EntityModel")
        {
        }
        
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles);
            
            modelBuilder.Entity<Content>()
                .HasMany(e => e.VotedUsers)
                .WithMany(e => e.VotedPublications);
        }
    }
}
