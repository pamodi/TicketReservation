using Microsoft.EntityFrameworkCore;
using Ticket.API.Data;
using Ticket.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<TicketService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
{
	policy.WithOrigins("https://ticket-reservation.runasp.net", "http://ticket-reservation.runasp.net", "https://localhost:7280")
	.AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

var app = builder.Build();

app.UseCors("CorsPolicy");

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/openapi/v1.json", "api");
		c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
	});
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
