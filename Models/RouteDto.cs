using System.ComponentModel.DataAnnotations;

namespace FunWithFlights.Models
{
	public record RouteDto(
		[Required] string Airline,
		[Required] string SourceAirport,
		[Required] string DestinationAirport,
		[Required] string CodeShare,
		[Required] int Stops,
		string? Equipment);
}
