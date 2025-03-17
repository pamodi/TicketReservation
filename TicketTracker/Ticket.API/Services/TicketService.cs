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
			TimeZoneInfo canadaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			return await _context.TicketReservation
								 .Where(u => !u.IsDeleted)
								 .Select(q => new ReservationsModel()
								 {
									 Id = q.Id,
									 Name = q.Name,
									 Email = q.Email,
									 PhoneNumber = q.PhoneNumber,
									 FormattedPhoneNumber = q.PhoneNumber,
									 IsStudent = q.IsStudent,
									 Tickets = q.NumberOfTickets,
									 ContactedBy = q.ContactedBy,
									 Comments = q.Comments,
									 CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(q.CreatedAt.DateTime, canadaTimeZone).ToString("yyyy/MM/dd hh:mm:ss tt")
								 }).ToListAsync();
		}

		public ReservationsModel GetReservationDetails(int reservationId)
		{
			var reservation = _context.TicketReservation
				.FirstOrDefault(q => q.Id == reservationId && !q.IsDeleted) ?? throw new InvalidOperationException("Reservation not found.");

			TimeZoneInfo canadaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			return new ReservationsModel()
			{
				Id = reservation.Id,
				Name = reservation.Name,
				Email = reservation.Email,
				PhoneNumber = UnFormatPhone(reservation.PhoneNumber),
				FormattedPhoneNumber = reservation.PhoneNumber,
				IsStudent = reservation.IsStudent,
				Tickets = reservation.NumberOfTickets,
				ContactedBy = reservation.ContactedBy,
				Comments = reservation.Comments,
				CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(reservation.CreatedAt.DateTime, canadaTimeZone).ToString("yyyy/MM/dd hh:mm:ss tt")
			};
		}

		private string UnFormatPhone(string phoneNumber)
		{
			if (phoneNumber == null)
				return string.Empty;

			string digitsOnly = new(phoneNumber.Where(char.IsDigit).ToArray());
			return digitsOnly;
		}

		public void UpdateReservation(int reservationId, UpdateReservationRequest updateReservationRequest)
		{
			var reservation = _context.TicketReservation.FirstOrDefault(q => q.Id == reservationId && !q.IsDeleted) ?? throw new InvalidOperationException("Reservation not found.");

			reservation.Name = updateReservationRequest.Name;
			reservation.Email = updateReservationRequest.Email;
			reservation.PhoneNumber = updateReservationRequest.PhoneNumber;
			reservation.IsStudent = updateReservationRequest.IsStudent;
			reservation.NumberOfTickets = updateReservationRequest.Tickets;
			reservation.ContactedBy = updateReservationRequest.ContactedBy;
			reservation.Comments = updateReservationRequest.Comments;

			_context.SaveChanges();
		}

		public void DeleteReservation(int reservationId)
		{
			var reservation = _context.TicketReservation.FirstOrDefault(q => q.Id == reservationId && !q.IsDeleted) ?? throw new InvalidOperationException("Reservation not found.");

			reservation.IsDeleted = true;
			reservation.UpdatedAt = DateTimeOffset.UtcNow;

			_context.SaveChanges();
		}
	}
}
