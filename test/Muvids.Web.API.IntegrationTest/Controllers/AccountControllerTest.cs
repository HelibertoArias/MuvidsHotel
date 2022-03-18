using Microsoft.AspNetCore.Mvc;
using Muvids.Application.Models.Authentication;
using Muvids.Web.API.IntegrationTest.Base.Identity;
using Muvids.Web.API.IntegrationTest.Controllers.Data;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Muvids.Web.API.IntegrationTest.Controllers;

public class AccountControllerTest : IClassFixture<IdentityCustomWebApplicationFactory<Program>>
{
    private readonly IdentityCustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private HttpClient _client;

    public AccountControllerTest(IdentityCustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;

        Reset();
    }

    private void Reset()
    {
        _client = _factory.GetAnonymousClient();
    }

    [Theory]
    [RegistrationRequestData]
    public async Task Authenticate_Should_Validate(RegistrationRequest input,  HttpStatusCode expectedStatusCode )
    {
        var json = JsonConvert.SerializeObject(input);

        var response = await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));
 
        var responseString = await response.Content.ReadAsStringAsync();
        _output.WriteLine(responseString);
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }



    [Fact]
    public async Task Authenticate_Should_Authenticate_An_Existing_User()
    {
        // Creatint the user first
        var registrationRequest = new RegistrationRequest()
        {

            FirstName = "Heliberto11",
            LastName = "Arias11",
            UserName = "helibertoarias11",
            Email = "helibertoarias11@gmail.com",
            Password = "P@ssword202"
        };

        var json = JsonConvert.SerializeObject(registrationRequest);

        await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));

        // Autehticate
        var authenticationRequest = new AuthenticationRequest()
        {

            Email = "helibertoarias11@gmail.com",
            Password = "P@ssword202"
        };


        json = JsonConvert.SerializeObject(authenticationRequest);

        var response = await _client.PostAsync("/api/account/authenticate", new StringContent(json, Encoding.UTF8, "application/json"));

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<RegistrationResponse>(responseString);

        Assert.IsType<RegistrationResponse>(result);
    }

    [Fact]
    public async Task Authenticate_Should_Authenticate_An_Existing_User_With_Wrong_Password()
    {
        // Creatint the user first
        var registrationRequest = new RegistrationRequest()
        {

            FirstName = "Heliberto22",
            LastName = "Arias22",
            UserName = "helibertoarias22",
            Email = "helibertoarias22@gmail.com",
            Password = "P@ssword202w"
        };

        var json = JsonConvert.SerializeObject(registrationRequest);

        var resultw = await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));

        // Autehticate
        var authenticationRequest = new AuthenticationRequest()
        {

            Email = "helibertoarias22@gmail.com",
            Password = "P@ssword202"
        };


        json = JsonConvert.SerializeObject(authenticationRequest);

        var response = await _client.PostAsync("/api/account/authenticate", new StringContent(json, Encoding.UTF8, "application/json"));

        var responseString = await response.Content.ReadAsStringAsync();


        Assert.Equal("Credentials for helibertoarias22@gmail.com aren't valid.", responseString);
    }

    [Fact]
    public async Task Register_Should_Create_New_Username()
    {
        var registrationRequest = new RegistrationRequest()
        {

            FirstName = "Heliberto33",
            LastName = "Arias33",
            UserName = "helibertoarias33",
            Email = "helibertoarias33@gmail.com",
            Password = "P@ssword202"
        };



        var json = JsonConvert.SerializeObject(registrationRequest);

        var response = await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<RegistrationResponse>(responseString);

        Assert.IsType<RegistrationResponse>(result);

    }

    [Fact]
    public async Task Register_Should_Throw_Error_With_Existing_Email()
    {
        // Arrange
        var registrationRequest = new RegistrationRequest()
        {
            FirstName = "Jane1",
            LastName = "Doe1",
            UserName = "janedoe1",
            Email = "janedoe1@gmail.com",
            Password = "P@ssword202"
        };


        var json = JsonConvert.SerializeObject(registrationRequest);

        var r = await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));

        // Act
        registrationRequest.UserName = "theJaneDoe";
        json = JsonConvert.SerializeObject(registrationRequest);

        var response = await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));
        var responseString = await response.Content.ReadAsStringAsync();
        _output.WriteLine(responseString);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("Email janedoe1@gmail.com already exists.", responseString);
    }

    [Fact]
    public async Task Register_Should_Throw_With_Existing_User()
    {
        // Arrange

        var registrationRequest = new RegistrationRequest()
        {

            FirstName = "Jane",
            LastName = "Doe",
            UserName = "janedoe",
            Email = "janedoe@gmail.com",
            Password = "P@ssword202"
        };

        var json = JsonConvert.SerializeObject(registrationRequest);

        await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));

        // Act
        var response = await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("Username 'janedoe' already exists.", responseString);
    }



    [Fact]
    public async Task Register_Should_Throw_Error_Password_Validation()
    {
        // Arrange

        var registrationRequest = new RegistrationRequest()
        {
            FirstName = "Jane2",
            LastName = "Doe2",
            UserName = "janedoe2",
            Email = "janedoe2@gmail.com",
            Password = "qweqwewqww"
        };

        var json = JsonConvert.SerializeObject(registrationRequest);

        // Act
        var response = await _client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));
        var responseString = await response.Content.ReadAsStringAsync();
        // Asert
        Assert.Contains("Failed", responseString);
    }
}
