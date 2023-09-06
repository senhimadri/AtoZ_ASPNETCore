﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationAuthorization.JWTAuthentication;

public static class JwtHelpers
{
    public static IEnumerable<Claim> GetClaims (this UserTokens userAccounts,Guid Id)
    {
        IEnumerable<Claim> claims = new List<Claim>()
        {
            new Claim ("Id", userAccounts.Id.ToString()),
            new Claim (ClaimTypes.Name, userAccounts.UserName),
            new Claim (ClaimTypes.Email, userAccounts.EmailId),
            new Claim (ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim (ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
        };
        return claims;
    }

    public static IEnumerable<Claim> GetClaims (this UserTokens userAccounts, out Guid Id)
    {
        Id = Guid.NewGuid ();
        return GetClaims(userAccounts, Id);
    }

    public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
    {
        try
        {
            var UserToken = new UserTokens();
            if (model is null) throw new ArgumentNullException( nameof(model));

            // Get Secret Key 
            var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigninKey);
            Guid Id = Guid.Empty;
            DateTime expireTime = DateTime.UtcNow.AddDays(1);
            UserToken.Validaty = expireTime.TimeOfDay;

            var JWTToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer,audience:jwtSettings.ValidAudience,claims: GetClaims(model,out Id),
                                notBefore: new DateTimeOffset(DateTime.Now).DateTime,expires: new DateTimeOffset(expireTime).DateTime,
                                signingCredentials:  new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256));

            UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWTToken);
            UserToken.UserName = model.UserName;
            UserToken.Id = model.Id;
            UserToken.GuidId = Id;
            return UserToken;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
