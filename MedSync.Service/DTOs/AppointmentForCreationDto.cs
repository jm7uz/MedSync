namespace MedSync.Service.DTOs;

public class AppointmentForCreationDto
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string Description { get; set; }
}
