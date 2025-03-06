using System.ComponentModel.DataAnnotations;

namespace Ticket.API.Entity
{
	public class Country : BaseEntity
	{
		public int Id { get; set; }

		[MaxLength(200)]
		public string CountryName { get; set; }
	}
}
