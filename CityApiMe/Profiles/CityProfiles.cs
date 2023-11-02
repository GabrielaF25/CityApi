using AutoMapper;
using System.Runtime.InteropServices;

namespace CityApiMe.Profiles
{
	public class CityProfiles:Profile
	{
		public CityProfiles() {
			CreateMap<Entities.City,Models.CityMod>();

			CreateMap<Entities.City, Models.CitiesWithoutPOI>();
			CreateMap<Models.PointOfInterestForCreationDto, Entities.City>();
		
		}
	}
}
