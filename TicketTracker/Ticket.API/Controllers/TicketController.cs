using Microsoft.AspNetCore.Mvc;
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
			_ticketService.Save(model);

			return Ok(new { Message = "Ticket reserved successfully" });
		}

		[HttpGet("lookup/contacts")]
		public async Task<IActionResult> GetAllLookupContacts()
		{
			return Ok(_ticketService.GetAllLookupContacts());
		}
	}
}
