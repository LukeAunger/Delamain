using System;

namespace Delamain_backend.Models;

public partial class IcuData
{
    [Key]
    public int Id { get; set; }

    public bool? Outcome { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; } = string.Empty;
    public double? Bmi { get; set; }
    public bool? Hypertensive { get; set; }
    public bool? Atrialfibrillation { get; set; }
    public bool? ChdWithNoMi { get; set; }
    public bool? Diabetes { get; set; }
    public bool? Deficiencyanemias { get; set; }
    public bool? Depression { get; set; }
    public bool? Hyperlipemia { get; set; }
    public bool? Copd { get; set; }
    public double? HeartRate { get; set; }
    public double? RespitoryRate { get; set; }
    public double? Temperature { get; set; }
}
