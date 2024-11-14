using Microsoft.EntityFrameworkCore;
using ParkMobileServer.Entities;

namespace ParkMobileServer.DbContext
{
	public class PostgreSQLDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public PostgreSQLDbContext(DbContextOptions<PostgreSQLDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<ItemEntity>().HasData(
				new ItemEntity { Id = 1, Image = "WQERwer", Price = "23 432", Tag = "Apple Watch 35mm" },
				new ItemEntity { Id = 2, Image = "qweqw", Price = "124 000", Tag = "Iphone 15 Pro Max" }
			);
		}

		public DbSet<ItemEntity> ItemEntities { get; set; } = null!;
	}
}
