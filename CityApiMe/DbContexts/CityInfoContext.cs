using CityApiMe.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityApiMe.DbContexts
{
	public class CityInfoContext:DbContext
	{
		 public DbSet<City> Cities { get; set; }
		public DbSet<PointOfInterest> PointOfInterests { get; set; } 

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlite("connectionstring");
		//	base.OnConfiguring(optionsBuilder);
		//}
		public CityInfoContext( DbContextOptions<CityInfoContext> option):base(option) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<City>().HasData(
				new City("New York City")
			{
				
				Id = 1,
				Description = "The One with the  big park."
			},
				new City("Antwerp")
				{
					Id = 2,
					Description = "The One with the cathedral that was never really finished."
				},
				new City("Paris")
				{
					Id = 3,
					Description = "The One with the  big tower."
				});
			modelBuilder.Entity<PointOfInterest>().HasData(
				new PointOfInterest("Central Park")
				{
					Id = 1,
					cityId = 1,
					Description = "The most visited urban park in the United States."
				},
				new PointOfInterest("Empire State building")
				{
					Id = 2,
					cityId = 1,
					Description = "A 102-story skyscraper located in Midtown Manhattan."
				},
				 new PointOfInterest("Cathedral")
				 {
					 Id = 3,
					 cityId = 2,
					 Description = "A Gothic style cathedral."
				 },
					 new PointOfInterest("Antwerp Central Station\"")
					 {
						 Id = 4,
						 cityId = 2,
						 Description = "The finest example of railway architecture in Blegium."
					 },
					 new PointOfInterest("Eiffel Tower")
					 {
						 Id = 5,
						 cityId = 3,
						 Description = "A Wrought iron lattice tower on the Champ de Mars."
					 },
					 new PointOfInterest("The Louvre")
					 {
						 Id = 6,
						 cityId = 3,
						 Description = "The world's largest museum."
					 }

			
						);
		}
	}
}
