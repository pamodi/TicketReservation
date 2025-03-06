namespace Ticket.API.Models
{ 
	public class UserEmailModel
	{
		public string Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
	}

	public class SendEmailRequest
	{
		public List<string> Recipients { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string? TemplateName { get; set; }
	}
}
