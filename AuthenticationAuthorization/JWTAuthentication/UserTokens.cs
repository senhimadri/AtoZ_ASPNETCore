namespace AuthenticationAuthorization.JWTAuthentication;

public class UserTokens
{
    public string Token { get; set; }=string.Empty;
    public string UserName { get; set; }=string.Empty;
    public TimeSpan Validaty { get; set; }
    public string RefreshToken { get; set; }=string.Empty;
    public Guid Id { get; set; }
    public string EmailId { get; set; }=string.Empty;
    public Guid GuidId { get; set; }
    public DateTime ExpiredTime { get; set; }
}
