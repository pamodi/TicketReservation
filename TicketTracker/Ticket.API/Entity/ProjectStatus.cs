﻿using System.ComponentModel.DataAnnotations;

namespace Ticket.API.Entity
{
	public class ProjectStatus : BaseEntity
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(200)]
		public string? Description { get; set; }
	}
}
