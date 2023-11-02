using AutoMapper;

namespace CityApiMe.Profiles
{
	public class PointOfInterestProfile:Profile
	{
		public PointOfInterestProfile() 
		{
			CreateMap<Entities.PointOfInterest, Models.PointsOfInterestCity>();
			CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
			CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdating>();
			CreateMap<Models.PointOfInterestForUpdating, Entities.PointOfInterest>();
		}

	}
}
