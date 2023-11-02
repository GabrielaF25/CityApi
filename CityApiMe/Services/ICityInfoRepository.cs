using CityApiMe.Entities;

namespace CityApiMe.Services
{
	public interface ICityInfoRepository
	{
		Task<IEnumerable<City>> GetCitiesAsync();

		Task<City?> GetCityAsync(int cityId, bool pointOfInterest);

		Task<IEnumerable<PointOfInterest>> GetPointsOfInterestCityAsync(int cityId);

		Task<PointOfInterest?> GetPointOfInterestCityAsync(int cityId, int pointOfInterestId);

		Task<bool> CityExistsAsync(int cityId);

		Task AddPointOfInterestForCityAsync(int cityId,
			PointOfInterest pointOfInterest);

		void DeletePointOfInterest(PointOfInterest pointOfInterest);

		Task<bool> SaveChangesAsync();
		Task<IEnumerable<City>> GetCitiesAsync(string? name,string?searchQuery);

	}
}
