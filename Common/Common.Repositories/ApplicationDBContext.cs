using Common.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Repositories
{
	public class ApplicationDBContext : DbContext
	{
		public DbSet<ToDoNode> Todos {get;set;}
		public DbSet<UserNode> Users { get; set; }

        public ApplicationDBContext( DbContextOptions<ApplicationDBContext> dbContext) : base (dbContext)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ToDoNode>().HasKey(k => k.Id);
			modelBuilder.Entity<ToDoNode>().Property(n => n.Label).HasMaxLength(200).IsRequired();

			modelBuilder.Entity<UserNode>().HasKey(k => k.Id);
			modelBuilder.Entity<UserNode>().Property(n => n.Name).HasMaxLength(200).IsRequired();

			modelBuilder.Entity<ToDoNode>()
				.HasOne(u => u.Owner)
				.WithMany(t => t.Todos)
				.HasForeignKey(u => u.OwnerId);


			base.OnModelCreating(modelBuilder);
		}
	}
}
