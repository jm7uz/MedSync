using MedSync.Domain.Commons;
using MedSync.Domain.Enums;

namespace MedSync.Domain.Entities;

public class Appointment : Auditable
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public DateTime ScheduledDateTime {  get; set; }
    public string Description { get; set; } = string.Empty;
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
}
