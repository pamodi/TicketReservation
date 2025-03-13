using System.ComponentModel.DataAnnotations;

namespace Ticket.API.Models
{
	public class ReserveTicketModel
	{
		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; } = "";

		//[RequiredIfEmpty(nameof(PhoneNumber), ErrorMessage = "Either Email or Phone must be provided.")]
		[Required(ErrorMessage = "Please enter your email.")]
		public string Email { get; set; } = "";

		//[RequiredIfEmpty(nameof(Email), ErrorMessage = "Either Phone or Email must be provided.")]
		[Required(ErrorMessage = "Please enter your phone number.")]
		public string PhoneNumber { get; set; } = "";

		public bool IsStudent { get; set; } = false;

		[Required(ErrorMessage = "Please enter the number of tickets.")]
		[Range(1, 150, ErrorMessage = "Tickets must be between 1 and 150.")]
		public int Tickets { get; set; } = 1;

		public string? ContactedBy { get; set; }

		public string? Comments { get; set; }
	}

	public class ReservationsModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string? Email { get; set; }

		public string? PhoneNumber { get; set; }

		public string? FormattedPhoneNumber { get; set; }

		public bool IsStudent { get; set; }

		public int Tickets { get; set; }

		public string? ContactedBy { get; set; }

		public string? Comments { get; set; }

		public string CreatedAt { get; set; }
	}

	public class UpdateReservationRequest
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter your name.")]
		public string Name { get; set; }

		public string? Email { get; set; }

		public string? PhoneNumber { get; set; }

		public bool IsStudent { get; set; }

		[Required(ErrorMessage = "Please enter the number of tickets.")]
		[Range(1, 150, ErrorMessage = "Tickets must be between 1 and 150.")]
		public int Tickets { get; set; }

		public string? ContactedBy { get; set; }

		public string? Comments { get; set; }
	}

	public class RequiredIfEmptyAttribute : ValidationAttribute
	{
		private readonly string _otherProperty;

		public RequiredIfEmptyAttribute(string otherProperty)
		{
			_otherProperty = otherProperty;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var currentValue = value as string;
			var otherPropertyValue = validationContext.ObjectType.GetProperty(_otherProperty)
									?.GetValue(validationContext.ObjectInstance) as string;

			if (string.IsNullOrEmpty(currentValue) && string.IsNullOrEmpty(otherPropertyValue))
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}
	}

}
