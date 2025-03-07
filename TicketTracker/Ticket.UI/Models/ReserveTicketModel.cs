﻿using System.ComponentModel.DataAnnotations;

namespace Ticket.UI.Models
{
	public class ReserveTicketModel
	{
		[Required(ErrorMessage = "Please enter your name.")]
		public string Name { get; set; } = "";

		[RequiredIfEmpty(nameof(Phone), ErrorMessage = "Either Email or Phone must be provided.")]
		public string Email { get; set; } = "";

		[RequiredIfEmpty(nameof(Email), ErrorMessage = "Either Phone or Email must be provided.")]
		public string Phone { get; set; } = "";

		public bool IsStudent { get; set; } = false;

		[Required(ErrorMessage = "Please enter the number of tickets.")]
		[Range(1, 150, ErrorMessage = "Tickets must be between 1 and 150.")]
		public int Tickets { get; set; } = 1;

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
