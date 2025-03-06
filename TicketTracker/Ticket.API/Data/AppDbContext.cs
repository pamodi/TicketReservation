using Ticket.API.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ticket.API.Data
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.Guid)
					  .HasDefaultValueSql("NEWID()");

				entity.HasIndex(e => e.Guid)
					  .IsUnique();

				entity.Property(e => e.CreatedAt)
					  .HasDefaultValueSql("SYSDATETIMEOFFSET()");
			});

			modelBuilder.Entity<User>()
						.HasOne(u => u.UserType)
						.WithMany()
						.HasForeignKey(u => u.UserTypeId)
						.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Project>()
						.HasOne(p => p.ProjectStatus)
						.WithMany()
						.HasForeignKey(p => p.ProjectStatusId)
						.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Project>()
						.HasOne(p => p.ProjectCategory)
						.WithMany()
						.HasForeignKey(p => p.ProjectCategoryId)
						.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<ProjectOwner>()
						.HasOne(u => u.Project)
						.WithMany()
						.HasForeignKey(u => u.ProjectId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProjectOwner>()
						.HasOne(u => u.User)
						.WithMany()
						.HasForeignKey(u => u.UserId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProjectContribution>()
						.HasOne(u => u.ProjectOwner)
						.WithMany()
						.HasForeignKey(u => u.ProjectOwnerId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProjectContribution>()
						.HasOne(u => u.PaymentMethod)
						.WithMany()
						.HasForeignKey(u => u.PaymentMethodId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProjectContribution>()
						.HasOne(u => u.PaymentStatus)
						.WithMany()
						.HasForeignKey(u => u.PaymentStatusId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Province>()
						.HasOne(u => u.Country)
						.WithMany()
						.HasForeignKey(u => u.CountryId)
						.OnDelete(DeleteBehavior.NoAction);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<UserType> UserTypes { get; set; }
		public DbSet<ProjectStatus> ProjectStatuses { get; set; }
		public DbSet<ProjectCategory> ProjectCategories { get; set; }
		public DbSet<EmailAudit> EmailAudits { get; set; }
		public DbSet<ProjectOwner> ProjectOwners { get; set; }
		public DbSet<ProjectContribution> ProjectContributions { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
		public DbSet<PaymentStatus> PaymentStatuses { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Province> Provinces { get; set; }
		public DbSet<TicketReservation> TicketReservation { get; set; }
		public DbSet<LookupContactedBy> LookupContactedBy { get; set; }
	}
}
