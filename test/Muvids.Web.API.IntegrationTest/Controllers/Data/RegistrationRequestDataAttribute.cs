using Muvids.Application.Models.Authentication;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Xunit.Sdk;

namespace Muvids.Web.API.IntegrationTest.Controllers.Data;

public class RegistrationRequestDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo method)
    {
        yield return new object[] { new RegistrationRequest() , HttpStatusCode.UnprocessableEntity };
        yield return new object[] { new RegistrationRequest() { FirstName = "Jane" }, HttpStatusCode.UnprocessableEntity };
        yield return new object[] { new RegistrationRequest() { LastName = "Doe" }, HttpStatusCode.UnprocessableEntity };
        yield return new object[] { new RegistrationRequest() { Password = "Sort" }, HttpStatusCode.UnprocessableEntity };
        yield return new object[] { new RegistrationRequest() { Password = "Sort", UserName = "janedoe", LastName = "janedoe@gmail" }, HttpStatusCode.UnprocessableEntity };
        yield return new object[] { new RegistrationRequest() { Password = "Sort", UserName = "janedoe", LastName = "janedoe@gmail" }, HttpStatusCode.UnprocessableEntity };
        yield return new object[] { new RegistrationRequest() { Password = "Sort", UserName = "janedoe", FirstName = "Jane", LastName = "Doe", Email = "janedoe@gmail.com" }, HttpStatusCode.BadRequest };
        yield return new object[] { new RegistrationRequest() { Password = "Passw0rd2022", UserName = "janedoe", FirstName = "Jane", LastName = "Doe", Email = "janedoe@gmail.com" }, HttpStatusCode.BadRequest };
        yield return new object[] { new RegistrationRequest() { Password= "Passw0rd2022", UserName="janedoe", FirstName="Jane", LastName="Doe", Email="janedoe@gmailcom" }, HttpStatusCode.BadRequest };
    }
}
