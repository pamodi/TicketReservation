using Microsoft.EntityFrameworkCore;
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

		public bool Save(ReserveTicketModel model)
		{
			if (string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.PhoneNumber))
			{
				return false;
			}
			else
			{
				var ticketEntity = new TicketReservation
				{
					Name = model.Name,
					Email = model.Email,
					PhoneNumber = model.PhoneNumber,
					IsStudent = model.IsStudent,
					NumberOfTickets = model.Tickets,
					ContactedBy = model.ContactedBy,
					Comments = model.Comments
				};

				_context.TicketReservation.Add(ticketEntity);
				_context.SaveChanges();

				return true;
			}
		}

		public List<string> GetAllLookupContacts()
		{
			return _context.LookupContactedBy.Select(q => q.Name).ToList();
		}

		public async Task<IEnumerable<ReservationsModel>> GetAllReservations()
		{
			return await _context.TicketReservation
								 .Where(u => !u.IsDeleted)
								 .Select(q => new ReservationsModel()
								 {
									 Id = q.Id,
									 Name = q.Name,
									 Email = q.Email,
									 PhoneNumber = q.PhoneNumber,
									 IsStudent = q.IsStudent,
									 Tickets = q.NumberOfTickets,
									 ContactedBy = q.ContactedBy,
									 Comments = q.Comments,
									 CreatedAt = q.CreatedAt
								 }).ToListAsync();
		}
	}
}
