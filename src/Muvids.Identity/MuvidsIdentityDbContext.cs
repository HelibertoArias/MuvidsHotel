using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Muvids.Identity.Models;

namespace Muvids.Identity;

public class MuvidsIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public MuvidsIdentityDbContext(DbContextOptions<MuvidsIdentityDbContext> options) : base(options)
    {

    }
}
