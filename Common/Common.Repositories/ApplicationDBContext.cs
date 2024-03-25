using Common.Domain;
using Microsoft.EntityFrameworkCore;


namespace Common.Repositories
{
	public class ApplicationDBContext : DbContext
	{
		public DbSet<ToDo> Todos {get;set;}
		public DbSet<User> Users { get; set; }

        public ApplicationDBContext( DbContextOptions<ApplicationDBContext> dbContext) : base (dbContext)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ToDo>().HasKey(k => k.Id);
			modelBuilder.Entity<ToDo>().Property(n => n.Label).HasMaxLength(200).IsRequired();

			modelBuilder.Entity<User>().HasKey(k => k.Id);
			modelBuilder.Entity<User>().Property(n => n.Name).HasMaxLength(200).IsRequired();
			modelBuilder.Entity<User>().HasIndex(n => n.Login).IsUnique();


			modelBuilder.Entity<ToDo>()
				.HasOne(u => u.Owner)
				.WithMany(t => t.ToDos)
				.HasForeignKey(u => u.OwnerId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
