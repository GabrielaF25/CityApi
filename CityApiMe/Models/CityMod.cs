namespace CityApiMe.Models
{
	public class CityMod
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public int NumberPointsofInterest
		{
			get { return PointsOfInterest.Count; }
		}
		public ICollection<PointsOfInterestCity> PointsOfInterest { get; set; } = new List<PointsOfInterestCity>() { };
	}
}
