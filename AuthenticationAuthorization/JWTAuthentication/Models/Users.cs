namespace AuthenticationAuthorization.JWTAuthentication.Models;

public class Users
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string EmailId { get; set; } = string.Empty;
    public string MyProperty { get; set; } = string.Empty;
}

public class UserLogins
{
    public UserLogins()
    {
    }
    public string MyProperty { get; set; } = string.Empty;
    public string Password { get; set; }= string.Empty;

}
