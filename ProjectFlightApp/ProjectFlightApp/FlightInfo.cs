namespace ProjectFlightApp
{
	/// <summary>
	/// Flight info from server, lighter version of the one from the database
	/// </summary>
	public class FlightInfo
	{
		/// <summary>
		/// Identifier broadcast by the aircraft, primary key
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Aircraft registration number
		/// </summary>
		public string RegistrationNumber { get; set; }

		/// <summary>
		/// Latitude over the ground
		/// </summary>
		public float Latitude { get; set; }

		/// <summary>
		/// Longitude over the ground
		/// </summary>
		public float Longitude { get; set; }

		/// <summary>
		/// Speed in knots
		/// </summary>
		public float Speed { get; set; }

		/// <summary>
		/// Speed in kilometers per hour
		/// </summary>
		public float SpeedKm => Speed * 1.852001f;

		/// <summary>
		/// Vertical speed in feet per minute
		/// </summary>
		public int VerticalSpeed { get; set; }

		/// <summary>
		/// Vertical speed in meters per second
		/// </summary>
		public int VerticalSpeedM => (int)(VerticalSpeed * 0.3048f);

		/// <summary>
		/// Internal enum used for <see cref="GetInfoFromLocation"/>
		/// </summary>
		private enum LocationInfo
		{
			City,
			Country
		}

		/// <summary>
		/// Internally used to format city and country from location string (<see cref="Departure"/> or <see cref="Destination"/>)
		/// </summary>
		/// <param name="info"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		private static string GetInfoFromLocation(string info, LocationInfo type)
		{
			// To avoid crashing
			if (info == null)
				return null;

			var i = info.Split(',');

			/*
			 * If length is 2: <code> <city>, <country>
			 * If length is 3: <code> <airport>, <city>, <country>
			 * If length is 4: <code> <airport>, <city1>, <city2>, <country>
			 */

			switch (i.Length)
			{
				// If it's not city, assume country
				case 2: return type == LocationInfo.City ? i[0].Substring(4).Trim() : i[1].Trim();
				case 3: return type == LocationInfo.City ? i[1].Trim() : i[2].Trim();
				case 4: return type == LocationInfo.City ? $"{i[1]}, {i[2]}".Trim() : i[3].Trim();

				// If not 2 or 3, something is wrong
				default: return null;
			}
		}

		/// <summary>
		/// Code and name of departure airport
		/// </summary>
		public string Departure { get; set; }

		/// <summary>
		/// Departure city. Null if <see cref="Departure"/> is null
		/// </summary>
		public string DepartureCity => GetInfoFromLocation(Departure, LocationInfo.City);

		/// <summary>
		/// Departure country. Null if <see cref="Departure"/> is null
		/// </summary>
		public string DepartureCountry => GetInfoFromLocation(Departure, LocationInfo.Country);

		/// <summary>
		/// Departure ID. Null if <see cref="Departure"/> is null
		/// </summary>
		public string DepartureId => Departure?.Split(' ')[0];

		/// <summary>
		/// Code and name of destination airport
		/// </summary>
		public string Destination { get; set; }

		/// <summary>
		/// Destination city. Null if <see cref="Destination"/> is null
		/// </summary>
		public string DestinationCity => GetInfoFromLocation(Destination, LocationInfo.City);

		/// <summary>
		/// Destination country. Null if <see cref="Destination"/> is null
		/// </summary>
		public string DestinationCountry => GetInfoFromLocation(Destination, LocationInfo.Country);

		/// <summary>
		/// Destination ID. Null if <see cref="Departure"/> is null
		/// </summary>
		public string DestinationId => Destination?.Split(' ')[0];
	}
}