using AuthenticationAuthorization.JWTAuthentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorization.JWTAuthentication;

public class AuthServices
{
    private readonly JwtSettings _jwtSettings;


    public AuthServices(JwtSettings jwtSettings)=> _jwtSettings = jwtSettings;

    private IEnumerable<Users> _loginUsers = new List<Users>()
    {
        new Users()
        {
            Id= Guid.NewGuid(),
            EmailId="admin@email.com",
            UserName="admin",
            Password="admin"
        }
    };


    public string GetToken(UserLogins userLogins)
    {
        try
        {
            return string.Empty;
        }
        catch (Exception)
        {
            throw;
        }

    }


}
