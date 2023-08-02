using System;
using System.ComponentModel.DataAnnotations;

namespace Delamain_backend.Models
{
	public class userReqmodel
	{
        [Key]
        public string userReqID { get; set; } = string.Empty;

        public int queueordernum { get; set; }
        public int pushbackcount { get; set; }
        public double Riskscore { get; set; }

        public string name { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
        public int age { get; set; }
        public string gender { get; set; } = string.Empty;
        public double BMI { get; set; }
        public bool diabetes { get; set; }
        public bool deficiencyanemias { get; set; }
        public bool hypertensive { get; set; }
        public bool hyperlipemia { get; set; }
        public bool atrialfibrillation { get; set; }
        public bool CHD_with_no_MI { get; set; }
        public bool COPD { get; set; }
        public bool depression { get; set; }
        public double heart_rate { get; set; }
        public double respitory_rate { get; set; }
        public double tempurature { get; set; }
        public string phone { get; set; } = string.Empty;
        public DateTime date { get; set; } = DateTime.UtcNow;
        public string geoloc { get; set; } = string.Empty;
    }
}

