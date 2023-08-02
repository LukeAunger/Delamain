using System;
namespace Delamain_backend.Models
{
	public class HospitalLocation
	{
        [Key]
        public int Id { get; set; }

        public string name { get; set; } = string.Empty;
        public double lat { get; set; }
        public double lng { get; set; }
    }
}

