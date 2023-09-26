using MedSync.Data.IRepositories;
using MedSync.Data.Repositories;
using MedSync.Domain.Entities;
using MedSync.Service.DTOs;
using MedSync.Service.Exceptions;
using MedSync.Service.Interfaces;

namespace MedSync.Service.Services;

public class AppointmentService : IAppointmentService
{
    private long _id;
    private readonly IRepository<Appointment> appointmentRepository = new Repository<Appointment>();
    public async Task GenerateIdAsync()
    {
        var appointments = await appointmentRepository.SelectAllAsync();
        if (appointments.Count == 0)
        {
            this._id = 1;
        }
        else
        {
            var user = appointments[appointments.Count - 1];
            this._id = ++user.Id;
        }
    }
    public async Task<AppointmentForResultDto> CreateAsync(AppointmentForCreationDto dto)
    {
        var appointment = (await this.appointmentRepository.SelectAllAsync()).FirstOrDefault(a => a.ScheduledDateTime == dto.ScheduledDateTime);
        if (appointment is not null)
            throw new CustomException(400, "Appointment is already exist");
        
        await GenerateIdAsync();

        var newAppointment = new Appointment()
        {
            Id = this._id,
            DoctorId = dto.DoctorId,
            PatientId = dto.PatientId,
            ScheduledDateTime = dto.ScheduledDateTime,
            Description = dto.Description,
        };

        var result = await appointmentRepository.InsertAsync(newAppointment);

        var mappedAppointment = new AppointmentForResultDto()
        {
            Id = result.Id,
            DoctorId = result.DoctorId,
            PatientId = result.PatientId,
            ScheduledDateTime = result.ScheduledDateTime,
            Description = result.Description,
        };

        return mappedAppointment;
    }

    public async Task<List<AppointmentForResultDto>> GetAllAsync()
    {
        var appointments = await this.appointmentRepository.SelectAllAsync();
        var result = new List<AppointmentForResultDto>();

        foreach (var appointment in appointments)
        {
            var mappedAppointment = new AppointmentForResultDto()
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                ScheduledDateTime = appointment.ScheduledDateTime,
                Description = appointment.Description,
            };
            result.Add(mappedAppointment);
        }

        return result;
    }

    public async Task<AppointmentForResultDto> GetByIdAsync(long id)
    {
        var appointment = await this.appointmentRepository.SelectByIdAsync(id);
        if (appointment is null)
            throw new CustomException(404, "Appointment is not found");

        var mappedAppointment = new AppointmentForResultDto()
        {
            Id = appointment.Id,
            DoctorId = appointment.DoctorId,
            PatientId = appointment.PatientId,
            ScheduledDateTime = appointment.ScheduledDateTime,
            Description = appointment.Description,
        };

        return mappedAppointment;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var appointment = await this.appointmentRepository.SelectByIdAsync(id);
        if (appointment is null)
            throw new CustomException(404, "Appointment is not found");

        return await this.appointmentRepository.DeleteAsync(id);
    }

    public async Task<AppointmentForResultDto> UpdateAsync(AppointmentForUpdateDto dto)
    {
        var appointment = await this.appointmentRepository.SelectByIdAsync(dto.Id);
        if (appointment is null)
            throw new CustomException(404, "Appointment is not found");

        var mappedAppointment = new Appointment()
        {
            Id = appointment.Id,
            DoctorId = appointment.DoctorId,
            PatientId = appointment.PatientId,
            ScheduledDateTime = appointment.ScheduledDateTime,
            Description = appointment.Description,
            UpdatedAt = DateTime.UtcNow
        };

        await this.appointmentRepository.UpdateAsync(mappedAppointment);

        var result = new AppointmentForResultDto()
        {
            Id = dto.Id,
            ScheduledDateTime = dto.ScheduledDateTime,
            Description = dto.Description,
        };

        return result;

    }
}
