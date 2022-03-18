using Muvids.Identity;

namespace Muvids.Web.API.IntegrationTest.Base.Identity;

public class IdentityUtilities
{
    public static void InitializeDbForTests(MuvidsIdentityDbContext context)
    {
        //context.Users.Add(new()
        //{
        //    FirstName = "Jane",
        //    LastName = "Doe",
        //    UserName = "janedoe",
        //    Email = "janedoe@gmail.com",
        //    EmailConfirmed = true,
        //    Id = "00000000-0000-0000-0000-000000000000"
        //});


        context.SaveChanges();
    }
}