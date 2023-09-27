using MedSync.Domain.Enums;

namespace MedSync.Service.DTOs;

public class UserForResultDto
{
    public long Id { get; set; }
    public Role role { get; set; } = Role.Patient;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
}
