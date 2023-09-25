using MedSync.Service.DTOs;
using MedSync.Service.Services;

namespace MedSync.Presentation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            AppointmentService appointmentService = new AppointmentService();
            AppointmentForCreationDto appointmentForCreationDto = new AppointmentForCreationDto()
            {
                DoctorId = 1,
                PatientId = 1,
                Description = "Test",
                ScheduledDateTime = DateTime.Now,
            };
            await appointmentService.CreateAsync(appointmentForCreationDto);
            
        }
    }
}