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
				string formattedNumber = FormatPhoneNumber(model.PhoneNumber);

				var ticketEntity = new TicketReservation
				{
					Name = model.Name,
					Email = model.Email,
					PhoneNumber = formattedNumber,
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

		public static string FormatPhoneNumber(string phoneNumber)
		{
			if (phoneNumber == null)
				return string.Empty;

			string digits = new string(phoneNumber.Where(char.IsDigit).ToArray());

			if (digits.Length == 10)
				return $"({digits.Substring(0, 3)})-{digits.Substring(3, 3)}-{digits.Substring(6, 4)}";

			return phoneNumber; // Return original if not 10 digits
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
