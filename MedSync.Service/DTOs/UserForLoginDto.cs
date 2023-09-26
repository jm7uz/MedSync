using MedSync.Domain.Enums;

namespace MedSync.Service.DTOs;

public class UserForLoginDto
{
    public long Id { get; set; }
    public Role role { get; set; };
}
