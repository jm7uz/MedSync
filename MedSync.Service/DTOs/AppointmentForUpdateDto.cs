namespace MedSync.Service.DTOs;

public class AppointmentForUpdateDto
{
    public long Id { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string Description { get; set; }
}
