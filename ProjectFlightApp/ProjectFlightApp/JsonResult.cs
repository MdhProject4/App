namespace ProjectFlightApp
{
	/// <summary>
	/// Simple JSON from server
	/// </summary>
	public struct JsonResult
	{
		public bool Error;

		public override string ToString() => $"Error: {Error}";
	}

	/// <summary>
	/// Response from /user/
	/// </summary>
	public struct JsonUserResult
	{
		public bool   Error;
		public string User;

		public override string ToString() => $"Error: {Error}, User: {User}";
	}

	/// <summary>
	/// Response from /api/getFlight
	/// </summary>
	public struct JsonFlightInfoResult
	{
		public bool Error;
		public FlightInfo Info;
	}
}