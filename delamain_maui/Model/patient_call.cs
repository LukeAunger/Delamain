using System;
using SQLite;
namespace delamain_maui.Models
{
	public class patient_call
	{
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string userReqid { get; set; } = string.Empty;

            public string name { get; set; } = string.Empty;
            public string Symptoms { get; set; } = string.Empty;
            public int age { get; set; }
            public string gender { get; set; } = string.Empty;
            public double BMI { get; set; }
            public bool diabetes { get; set; }
            public bool deficiencyanemias { get; set; }
            public bool hypertensive { get; set; }
            public bool hyperlipemia { get; set; }
            public bool atrialfibrilation { get; set; }
            public bool CHD_with_no_MI { get; set; }
            public bool COPD { get; set; }
            public bool depression { get; set; }
            public int heart_rate { get; set; }
            public int respitory_rate { get; set; }
            public int tempurature { get; set; }
            public string phone { get; set; } = string.Empty;
            public DateTime date { get; set; } = DateTime.UtcNow;
            public string geoloc { get; set; } = string.Empty;
    }
}

