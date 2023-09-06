using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationAuthorization.JWTAuthentication;

public static class AddJWTTokenServicesExtensions
{
    public static void  AddJWTTokenServices(this IServiceCollection Services, IConfiguration Configuration)
    {
        // Add JWT Settings
        var bindJWTSettings = new JwtSettings();
        Configuration.Bind("JsonWebTokenKeys", bindJWTSettings);
        Services.AddSingleton(bindJWTSettings);

        Services.AddAuthentication(options=>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options=>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey=bindJWTSettings.ValidateIssuerSigninKey,
                IssuerSigningKey= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJWTSettings.IssuerSigninKey)),
                ValidateIssuer=bindJWTSettings.ValidateIssuer,
                ValidIssuer=bindJWTSettings.ValidIssuer,
                ValidateAudience=bindJWTSettings.ValidateAudience,
                ValidAudience=bindJWTSettings.ValidAudience,
                RequireExpirationTime=bindJWTSettings.RequireExpirationTime,
                ValidateLifetime=bindJWTSettings.ValidateLifeTime,
                ClockSkew=TimeSpan.FromDays(1)
            };
        });
    }
}
