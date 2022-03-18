using Microsoft.AspNetCore.Identity;

namespace Muvids.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}