using System;


namespace delamain_maui.Models
{
	public class HospitalLocations
	{
		[PrimaryKey]
		public int Id { get; set; }

		public string name { get; set; }
		public double lat { get; set; }
		public double lng { get; set; }
	}
}

