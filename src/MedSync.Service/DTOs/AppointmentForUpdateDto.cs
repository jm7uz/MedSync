using MedSync.Domain.Enums;

namespace MedSync.Service.DTOs;

public class AppointmentForUpdateDto
{
    public long Id { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string Description { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
}
