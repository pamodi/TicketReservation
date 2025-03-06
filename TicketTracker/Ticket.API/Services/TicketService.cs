using Ticket.API.Data;
using Ticket.API.Entity;
using Ticket.API.Models;

namespace Ticket.API.Services
{
	public class TicketService
	{
		private readonly AppDbContext _context;

		public TicketService(AppDbContext context)
		{
			_context = context;
		}

		public void Save(ReserveTicketModel model)
		{
			var ticketEntity = new TicketReservation
			{
				Name = model.Name,
				Email = model.Email,
				PhoneNumber = model.Phone,
				IsStudent = model.IsStudent,
				NumberOfTickets = model.Tickets,
				ContactedBy = model.ContactedBy,
				Comments = model.Comments
			};

			_context.TicketReservation.Add(ticketEntity);
			_context.SaveChanges();
		}

		public List<string> GetAllLookupContacts()
		{
			return _context.LookupContactedBy.Select(q => q.Name).ToList();
		}
	}
}
