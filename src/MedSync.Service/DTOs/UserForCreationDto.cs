using System.ComponentModel.DataAnnotations;

namespace MedSync.Service.DTOs;

public class UserForCreationDto
{
    [Required(ErrorMessage = "Enter the FirstName")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Enter the LastName")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Enter the Birth Day")]
    public DateTime DateOfBirth { get; set; }

    [EmailAddress(ErrorMessage = "Enter properly")]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
}
