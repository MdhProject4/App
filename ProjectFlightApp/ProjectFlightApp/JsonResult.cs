namespace ProjectFlightApp
{
	/// <summary>
	/// Simple JSON from server
	/// </summary>
	public struct JsonResult
	{
		public bool Error;
	}

	/// <summary>
	/// Response from /user/
	/// </summary>
	public struct JsonUserResult
	{
		public bool   Error;
		public string User;
	}
}