using MedSync.Service.DTOs;
using MedSync.Service.Services;

namespace MedSync.Presentation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //AppointmentService appointmentService = new AppointmentService();
            //AppointmentForCreationDto appointmentForCreationDto = new AppointmentForCreationDto()
            //{
            //    DoctorId = 1,
            //    PatientId = 1,
            //    Description = "Bosh og'riq",
            //    ScheduledDateTime = DateTime.Now,
            //};
            //await appointmentService.CreateAsync(appointmentForCreationDto);

            UserForCreationDto cr = new UserForCreationDto()
            {
                FirstName = "Test",
                LastName = "test",
                Email = "test@test.com",
                Password = "1234",
                PhoneNumber = "98789797",
                DateOfBirth = DateTime.Now,
            };

            UserService userService = new UserService();
            await userService.CreateAsync(cr);

        }
    }
}