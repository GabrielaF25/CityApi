using CityApiMe.DbContexts;
using CityApiMe.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityApiMe.Services
{
	public class CityInfoRepository : ICityInfoRepository
	{
		private readonly CityInfoContext context;

		public CityInfoRepository(CityInfoContext context)
		{
			this.context = context;
		}

		public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
		{
			var city = await GetCityAsync(cityId);
			if (city != null)
			{
				city.PointsOfInterest.Add(pointOfInterest);
			}
		}

		public async Task<bool> CityExistsAsync(int cityId)
		{
			return await context.Cities.AnyAsync();
		}

		public void DeletePointOfInterest(PointOfInterest pointOfInterest)
		{
			context.PointOfInterests.Remove(pointOfInterest);
		}

		public async Task<IEnumerable<City>> GetCitiesAsync()
		{
			return await context.Cities.OrderBy(c=>c.Name).ToListAsync();
		}

		public async Task<IEnumerable<City>> GetCitiesAsync(string? name, string?searchQuery)
		{
			var collection = context.Cities as IQueryable<City>;

			if (!string.IsNullOrWhiteSpace(name))
			{
				name = name.Trim();
				collection=collection.Where(c => c.Name == name);
			}
			if (!string.IsNullOrWhiteSpace(searchQuery))
			{
				searchQuery = searchQuery.Trim();
				collection = collection.Where(p => p.Name.Contains(searchQuery)
				||(p.Description!=null && p.Description.Contains(searchQuery)));
			}
			return await collection.OrderBy(p => p.Name).ToListAsync();
		}

		public async Task<City?> GetCityAsync(int cityId, bool pointOfInterest=false)
		{
			if (pointOfInterest)
			{
				return await context.Cities.Include(p => p.PointsOfInterest)
					.Where(c => c.Id == cityId).FirstOrDefaultAsync();
			}
			return await context.Cities
						.Where(c => c.Id == cityId)
						.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestCityAsync(int cityId)
		{
			return await context.PointOfInterests.Where(p => p.City.Id == cityId).ToListAsync();
		}

		public async Task<PointOfInterest?> GetPointOfInterestCityAsync(int cityId, int pointOfInterestId)
		{
			return await context.PointOfInterests.Where(p => p.City.Id == cityId &&
			p.Id == pointOfInterestId).FirstOrDefaultAsync();
		}

		public async Task<bool> SaveChangesAsync()
		{
			return (await context.SaveChangesAsync() >= 0);
		}
	}
}
