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

		[HttpGet("reservations", Name = "GetReservations")]
		[ProducesResponseType(typeof(IEnumerable<ReservationsModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllReservations()
		{
			var reservations = await _ticketService.GetAllReservations();
			return Ok(reservations);
		}


		[HttpGet("reservation/{reservationId}", Name = "GetReservationDetails")]
		[ProducesResponseType(typeof(ReservationsModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetReservationDetails([FromRoute] int reservationId)
		{
			return Ok(_ticketService.GetReservationDetails(reservationId));
		}

		[HttpPut("reservation/{reservationId}", Name = "UpdateReservation")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateReservation([FromRoute] int reservationId, [FromBody] UpdateReservationRequest updateReservationRequest)
		{
			_ticketService.UpdateReservation(reservationId, updateReservationRequest);
			return Ok();
		}

		[HttpDelete("reservation/{reservationId}", Name = "DeleteReservation")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult DeleteReservation([FromRoute] int reservationId)
		{
			_ticketService.DeleteReservation(reservationId);
			return Ok();
		}
	}
}
