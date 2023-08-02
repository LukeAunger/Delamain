using System;
namespace Delamain_backend.Models
{
	public class Queuemodel
	{
        [Key]
        public string queueID { get; set; } = string.Empty;

        public int queueordernum { get; set; }
        public int pushbackcount { get; set; }
        public double Riskscore { get; set; }

        public Userdetail? userdetail { get; set; }
    }
}

