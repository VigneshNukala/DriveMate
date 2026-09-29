namespace DriveMate.Domain.Models.DriverRequests;

public class DriverRequest
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid? DriverId { get; set; }

    public string PickupLocation { get; set; } = string.Empty;

    public string? DropLocation { get; set; }

    public DateOnly ServiceDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public decimal DurationHours { get; set; }

    public string? Notes { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}