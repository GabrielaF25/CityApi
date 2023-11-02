using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityApiMe.Entities
{
	public class PointOfInterest
	{
		[Key]

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

		public int Id { get; set; }

		[Required]

		[MaxLength(50)]

		public string Name { get; set; }

		[MaxLength(200)]

		public string? Description { get; set; }

		[ForeignKey("cityId")]

		public City? City { get; set; }

		public int? cityId { get; set; }

		public PointOfInterest(string name)
		{
			Name = name;
		}
	}
}
