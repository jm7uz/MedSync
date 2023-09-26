using MedSync.Service.DTOs;

namespace MedSync.Service.Interfaces;

public interface IAppointmentService
{
    public Task<AppointmentForResultDto> CreateAsync(AppointmentForCreationDto dto);
    public Task<AppointmentForResultDto> UpdateAsync(AppointmentForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<AppointmentForResultDto> GetByIdAsync(long id);
    public Task<List<AppointmentForResultDto>> GetAllAsync();
}
