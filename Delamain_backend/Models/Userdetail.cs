using System;
using Newtonsoft.Json;

namespace Delamain_backend.Models
{
	public class Userdetail
	{
        [Key]
        public string userReqID { get; set; } = string.Empty;

        public string name { get; set; } = "null";
        public string Symptoms { get; set; } = "null";
        public int age { get; set; }
        public string gender { get; set; } = "null";
        public double BMI { get; set; }
        public bool diabetes { get; set; } = false;
        public bool deficiencyanemias { get; set; } = false;
        public bool hypertensive { get; set; } = false;
        public bool hyperlipemia { get; set; } = false;
        public bool atrialfibrillation { get; set; } = false;
        public bool CHD_with_no_MI { get; set; } = false;
        public bool COPD { get; set; } = false;
        public bool depression { get; set; } = false;
        public double heart_rate { get; set; }
        public double respitory_rate { get; set; }
        public double tempurature { get; set; }
        public DateTime date { get; set; } = DateTime.UtcNow;
        public string geoloc { get; set; } = "null";
        public string phone { get; set; } = string.Empty;

        public string queueId { get; set; } = string.Empty;
        public Queuemodel Queuemodel { get; set; } = null!;
    }
}

