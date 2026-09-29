using System.ComponentModel.DataAnnotations;

namespace DriveMate.Application.DTOs.DriverRequests;

public class CreateDriverRequestRequest
{
    [Required]
    [StringLength(500)]
    public string PickupLocation { get; set; } = string.Empty;

    [StringLength(500)]
    public string? DropLocation { get; set; }

    [Required]
    public DateOnly ServiceDate { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Range(0.5, 24)]
    public decimal DurationHours { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }
}