using CityApiMe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityApiMe.Controllers
{
	[ApiController]
	[Route("api/cities")]
	
	public class CitiesController : ControllerBase
	{
		private readonly CitiesDataStores citiesDataStores;

		public CitiesController(CitiesDataStores citiesDataStores)
		{
			this.citiesDataStores = citiesDataStores;
		}
		[HttpGet]
		public ActionResult<IEnumerable<City>> GetCities()
		{
			return Ok(citiesDataStores.Cities);

		}
		[HttpGet("{id}")]
		public ActionResult<City> GetCity(int id)
		{
			var city = citiesDataStores.Cities.FirstOrDefault(p => p.Id == id);
			if(city == null)
			{
				return NotFound();
			}
			return Ok(city);
		}
	}
}
