namespace FunWithFlights.Data
{
	public record Route(
		string Airline, 
		string SourceAirport, 
		string DestinationAirport, 
		string CodeShare, 
		int Stops, 
		string Equipment);
}
