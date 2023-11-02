using AutoMapper;
using CityApiMe.DbContexts;
using CityApiMe.Models;
using CityApiMe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityApiMe.Controllers
{
	[ApiController]
	[Route("api/cities")]
	
	public class CitiesController : ControllerBase
	{
		private ICityInfoRepository cityInfoRepository;
		private readonly IMapper mapper;

		public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
		{
			this.cityInfoRepository = cityInfoRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CitiesWithoutPOI>>> GetCities(string? name, string?searchQuery)
		{
			var city = await cityInfoRepository.GetCitiesAsync(name,searchQuery);
			
			return Ok(mapper.Map<IEnumerable<CitiesWithoutPOI>>(city));

		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CitiesWithoutPOI>> GetCity(int id, bool pointOfInterest)
		{
			var city = await cityInfoRepository.GetCityAsync(id,pointOfInterest);
			if(city == null)
			{ 
				return NotFound(); 
			}
			if (pointOfInterest){

				return Ok(mapper.Map<CityMod>(city));
			}
			else 

			return Ok(mapper.Map<CitiesWithoutPOI>(city));
		}
	}
}
