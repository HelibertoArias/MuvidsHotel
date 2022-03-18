using System.ComponentModel.DataAnnotations;

namespace Muvids.Application.Models.Authentication;

public class RegistrationRequest
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    
    public string Email { get; set; } = null!;

    
    public string UserName { get; set; } = null!;

    
    
    public string Password { get; set; } = null!;
}
