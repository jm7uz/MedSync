using MedSync.Domain.Commons;
using MedSync.Domain.Enums;

namespace MedSync.Domain.Entities;

public class User : Auditable
{
    public Role role { get; set; } = Role.Patient;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}
