namespace CityApiMe.Models
{
	public class CitiesDataStores
	{
		public List<City> Cities { get; set; }

		public CitiesDataStores() {
				Cities=new List<City>()
		{			new City()
				{
					Id= 1,
					Name="New York City",
					Description="The one with that big park.",
				 PointsOfInterest=new List<PointsOfInterestCity>()
				 {
					 new PointsOfInterestCity()
					 {
						 Id=1,
						 Name="Central Park",
						 Description="The most visited urban park in the United States."
					 },
					 new PointsOfInterestCity()
					 {
						 Id=2,
						 Name="Empire State  Building",
						 Description="A 102-story skyscraper located in Midtown Manhattan."
					 }
				 }
				},
				new City()
				{
					Id= 2,
					Name="Antwerp",
					Description="The one with the cathedral that was never really finished.",
					PointsOfInterest=new List<PointsOfInterestCity>()
				 {
					 new PointsOfInterestCity()
					 {
						 Id=3,
						 Name="Cathedral of Our Lady",
						 Description="A Gothic style cathedral."
					 },
					 new PointsOfInterestCity()
					 {
						 Id=4,
						 Name="Antwerp Central Station",
						 Description="The finest example of railway architecture in Blegium."
					 }
				 }
				},
				new City()
				{
					Id= 3,
					Name="Paris",
					Description="The one with   that big tower.",
					PointsOfInterest=new List<PointsOfInterestCity>()
				 {
					 new PointsOfInterestCity()
					 {
						 Id=5,
						 Name="Eiffel Tower",
						 Description="A Wrought iron lattice tower on the Champ de Mars."
					 },
					 new PointsOfInterestCity()
					 {
						 Id=6,
						 Name="The Louvre",
						 Description="The world's largest museum."
					 }
				 }
				},
			};
		}
	}
}
