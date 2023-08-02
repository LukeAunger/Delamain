using System;
using System.ComponentModel.DataAnnotations;

namespace Delamain_backend.Models
{
	public class Riskmodel
	{
		[Key]
		public int ID { get; set; }

		public double age10to50 {get;set;}
		public double age51to100 { get; set; }
        public double male { get; set; }
		public double female { get; set; }
		public double BMI18to25 { get; set; }
		public double BMIoutsideof18to25 { get; set; }
		public double hypertensive { get; set; }
		//retinal vascular damage
		public double atrialfibrillation { get; set; }
		//abnormal heart rythm arrhythmia
		public double CHD_with_no_MI { get; set; }
		//Coronary Heart Disease with no Myocardial Infraction
		public double diabetes { get; set; }
		public double deficiencyanemias { get; set; }
		//iron deficiency
		public double depression { get; set; }
		public double hyperlipmia { get; set; }
		//high cholesterol
		public double COPD { get; set; }
		//chronic obstructive pulmonary
		public double hr60to100 { get; set; }
		//heart rate
		public double hroutside60to100 { get; set; }
		public double rr12to16 { get; set; }
		//respitory rate
		public double rroutside12to16 { get; set; }
		public double goodbodytemp{ get; set; }
		// good body tempurature is 36.1 to 37.2
		public double outsidegoodbodytemp { get; set; }
	}
}

