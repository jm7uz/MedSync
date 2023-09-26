namespace MedSync.Service.DTOs;

public class AppointmentForResultDto
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string Description { get; set; }
}
