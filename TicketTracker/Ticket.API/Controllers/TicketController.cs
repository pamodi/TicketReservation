﻿using Microsoft.AspNetCore.Mvc;
using Ticket.API.Models;
using Ticket.API.Services;

namespace Ticket.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketController : ControllerBase
	{
		private readonly TicketService _ticketService;

		public TicketController(TicketService ticketService)
		{
			_ticketService = ticketService;
		}

		[HttpPost("reserve")]
		public IActionResult Post([FromBody] ReserveTicketModel model)
		{
			var isSuccess = _ticketService.Save(model);
			if (isSuccess)
				return Ok(new { Message = "Ticket reserved successfully" });
			else
				return BadRequest(new { Message = "Please provide either email or phone" });
		}

		[HttpGet("lookup/contacts")]
		public async Task<IActionResult> GetAllLookupContacts()
		{
			return Ok(_ticketService.GetAllLookupContacts());
		}
	}
}
