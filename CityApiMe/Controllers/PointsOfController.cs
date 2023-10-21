using CityApiMe.Models;
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
		private CitiesDataStores _dataStores;
		private readonly ILocalMailService localMailService;

		public PointsOfController(ILogger<PointsOfInterestCity> logger,
			CitiesDataStores citiesDataStores, ILocalMailService localMailService) 
		{
			_dataStores = citiesDataStores;
			this.localMailService = localMailService;
			_logger = logger;
		}
 		[HttpGet]
		public ActionResult<IEnumerable<PointsOfInterestCity>> GetPointsOfInterest(int cityId)
		{
			var city = _dataStores.Cities.FirstOrDefault(p => p.Id == cityId);
			try
			{
				if (city == null)
				{
					_logger.LogInformation($"City with the id {cityId} wasn't found when accessing points of interest.");
					throw new Exception();
				}
			}
			catch(Exception ex) 
			{
				_logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);

				return StatusCode(500, "A problem happened while handling your request.");
			}
			return Ok(city.PointsOfInterest);
		}
		[HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
		public ActionResult<PointsOfInterestCity> GetPointOfInterest(int cityId, int pointOfInterestId)
		{
			var city = _dataStores.Cities.FirstOrDefault(p => p.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}
			var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
			if (pointOfInterest == null)
			{
				return NotFound();
			}
			return Ok(pointOfInterest);


		}
		[HttpPost]
		public ActionResult<PointsOfInterestCity> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
		{

			var city = _dataStores.Cities.FirstOrDefault(p => p.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}
			var maxId = _dataStores.Cities.SelectMany(p => p.PointsOfInterest).Max(p => p.Id);
			var finalPointToAdd = new PointsOfInterestCity()
			{
				Id = ++maxId,
				Name = pointOfInterest.Name,
				Description = pointOfInterest.Description,

			};
			city.PointsOfInterest.Add(finalPointToAdd);
			return CreatedAtRoute("GetPointOfInterest",
				new {
					cityId = cityId,
					pointOfInterestId = finalPointToAdd.Id
				}, finalPointToAdd);

		}
		[HttpPut("pointofinterestId")]
		public ActionResult EditingPointOfInterest(int cityId, int pointofinterestId,
			PointOfInterestForUpdating pointOfInterestForUpdating)
		{
			var city = _dataStores.Cities.FirstOrDefault(p => p.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}
			var updatedPointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointofinterestId);
			if (updatedPointOfInterest == null)
			{
				return NotFound();
			}
			updatedPointOfInterest.Name = pointOfInterestForUpdating.Name;
			updatedPointOfInterest.Description = pointOfInterestForUpdating.Description;
			return NoContent();
		}
		[HttpPatch("{pointofinterestId}")]
		public ActionResult EditingPartially(int cityId, int pointofinterestId,
			JsonPatchDocument<PointOfInterestForUpdating> patchDocument)
		{
			var city = _dataStores.Cities.FirstOrDefault(p => p.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}
			var updatedPointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointofinterestId);
			if (updatedPointOfInterest == null)
			{
				return NotFound();
			}
			var pointofinterestToPatch = new PointOfInterestForUpdating()
			{
				Name = updatedPointOfInterest.Name,
				Description = updatedPointOfInterest.Description,
			};
			patchDocument.ApplyTo(pointofinterestToPatch, ModelState);
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			if (!TryValidateModel(pointofinterestToPatch))
			{
				return BadRequest();
			}
			updatedPointOfInterest.Name = pointofinterestToPatch.Name;
			updatedPointOfInterest.Description = pointofinterestToPatch.Description;
			return NoContent();



		}
		[HttpDelete("{pointofinterestId}")]
		public ActionResult DeletePointofInterest(int cityId, int pointofinterestId)
		{
			var city = _dataStores.Cities.FirstOrDefault(p => p.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}
			var deletepointofinterest=city.PointsOfInterest.FirstOrDefault(p=>p.Id == pointofinterestId);
			if(deletepointofinterest == null) 
			{
				return NotFound();
			}
			localMailService.Send("Point of interest deleted.",
				$"Point ofinterest {deletepointofinterest.Name} with id" +
				$" {deletepointofinterest.Id} was deleted");
			city.PointsOfInterest.Remove(deletepointofinterest);
			return NoContent();


		}
	}
}
