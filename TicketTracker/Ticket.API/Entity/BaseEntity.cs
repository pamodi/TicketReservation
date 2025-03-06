namespace Ticket.API.Entity
{
	public class BaseEntity
	{
		public bool IsDeleted { get; set; } = false;

		public DateTimeOffset CreatedAt { get; internal set; }

		public DateTimeOffset? UpdatedAt { get; set; }

		public BaseEntity()
		{
			CreatedAt = DateTimeOffset.UtcNow;
		}
	}
}
