namespace Ticket.API.Models
{
	public class CreateProjectContributionRequest
	{
		public string UserId { get; set; }
		public decimal Amount { get; set; }
		public DateTimeOffset PaidDate { get; set; }
		public string PaymentMethod	{ get; set; }
		public string PaymentStatus { get; set; }
		public string? PaymentReference { get; set; }
		public string? AdditionalNote { get; set; }
	}

	public class CreateProjectContributionResponse
	{
		public int Id { get; set; }
	}

	public class DonationModel
	{
		public string DonatedUserName { get; set; }
		public string? DonatedUserEmail { get; set; }
		public decimal DonatedAmount { get; set; }
		public string DonationMethod { get; set; }
		public string DonationStatus { get; set; }
		public DateTime? PaidDate { get; set; }
		public string? DonationReference { get; set; }
		public string? DonationNote { get; set; }
	}
}
