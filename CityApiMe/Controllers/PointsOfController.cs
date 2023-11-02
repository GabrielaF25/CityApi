using AutoMapper;
using CityApiMe.Entities;
using CityApiMe.Models;
using CityApiMe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityApiMe.Controllers
{
	[Route("api/cities/{cityId}/pointsofinterest")]
	[ApiController]
	public class PointsOfController : ControllerBase
	{
         private ILogger<PointsOfInterestCity> _logger;

	

		private readonly ILocalMailService localMailService;
		private readonly IMapper mapper;
		private readonly ICityInfoRepository cityInfoRepository;

		public PointsOfController(ILogger<PointsOfInterestCity> logger,
			 ILocalMailService localMailService, IMapper mapper
			,ICityInfoRepository cityInfoRepository)
		{
			
			this.localMailService = localMailService;
			this.mapper = mapper;
			this.cityInfoRepository = cityInfoRepository;
			_logger = logger;
		}
		[HttpGet]

		public async Task<ActionResult<IEnumerable<PointsOfInterestCity>>> GetPointsOfInterest(int cityId)
		{
		 
			try
			{
				if (!await cityInfoRepository.CityExistsAsync(cityId))
				{
					_logger.LogInformation($"City with the id {cityId} wasn't found when accessing points of interest.");
					throw new Exception();
				}
			}
			catch (Exception ex)
			{
				_logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);

				return StatusCode(500, "A problem happened while handling your request.");
			}
			var pointsOfCity= await cityInfoRepository.GetPointsOfInterestCityAsync(cityId);
			return Ok(mapper.Map<IEnumerable<PointsOfInterestCity>>(pointsOfCity));
			
		}

		[HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]

		public async Task <ActionResult<PointsOfInterestCity>> GetPointOfInterest(int cityId, int pointOfInterestId)
		{
			
			if (! await cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}
			var pointOfInterest = await cityInfoRepository.GetPointOfInterestCityAsync(cityId, pointOfInterestId);	
			if (pointOfInterest == null)
			{
				return NotFound();
			}
			return Ok(mapper.Map<PointsOfInterestCity>(pointOfInterest));


		}

		[HttpPost]

		public async Task<ActionResult<PointsOfInterestCity>> 
			CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
		{

			
			if (! await cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}


			var pointOfInterestEntity = mapper.Map<PointOfInterest>(pointOfInterest);
			await cityInfoRepository.AddPointOfInterestForCityAsync(cityId, pointOfInterestEntity);
			await cityInfoRepository.SaveChangesAsync();
			var finalPointToAdd = mapper.Map<PointsOfInterestCity>(pointOfInterestEntity);
			
			return CreatedAtRoute("GetPointOfInterest",
				new
				{
					cityId = cityId,
					pointOfInterestId = finalPointToAdd.Id
				}, finalPointToAdd);

		}

		[HttpPut("pointofinterestId")]

		public async Task<ActionResult> EditingPointOfInterest(int cityId, int pointofinterestId,
			PointOfInterestForUpdating pointOfInterestForUpdating)
		{
			
			if (! await cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}
			var updatedPointOfInterest = await cityInfoRepository.GetPointOfInterestCityAsync(cityId, pointofinterestId);
			if (updatedPointOfInterest == null)
			{
				return NotFound();
			}
				mapper.Map(pointOfInterestForUpdating, updatedPointOfInterest);

			await cityInfoRepository.SaveChangesAsync();

			return NoContent();
		}
		[HttpPatch("{pointofinterestId}")]
		public async Task<ActionResult> EditingPartially(int cityId, int pointofinterestId,
			JsonPatchDocument<PointOfInterestForUpdating> patchDocument)
		{
			
			if (! await cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}
			var updatedPointOfInterest = await cityInfoRepository.GetPointOfInterestCityAsync(cityId, pointofinterestId);
			if (updatedPointOfInterest == null)
			{
				return NotFound();
			}
			var pointofinterestToPatch = mapper.Map<PointOfInterestForUpdating>(updatedPointOfInterest);
			
			patchDocument.ApplyTo(pointofinterestToPatch, ModelState);
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			if (!TryValidateModel(pointofinterestToPatch))
			{
				return BadRequest();
			}
			mapper.Map(pointofinterestToPatch,updatedPointOfInterest);

			await cityInfoRepository.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{pointofinterestId}")]

		public async Task<ActionResult> DeletePointofInterest(int cityId, int pointofinterestId)
		{
			
			if (! await cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}
			var pointOfInterestForDelete = await cityInfoRepository
				.GetPointOfInterestCityAsync(cityId, pointofinterestId);
			

			if (pointOfInterestForDelete == null)
			{
				return NotFound();
			}

			cityInfoRepository.DeletePointOfInterest(pointOfInterestForDelete);

			await cityInfoRepository.SaveChangesAsync();

			localMailService.Send("Point of interest deleted.",
				$"Point ofinterest {pointOfInterestForDelete.Name} with id" +
				$" {pointOfInterestForDelete.Id} was deleted");
		
			return NoContent();

		}
	}
}
