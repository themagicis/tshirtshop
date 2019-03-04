using App.Common.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TShirtShop.Server.Security
{
    public class JwtTokenProvider
    {
        private readonly ISecurityKeyProvider securityKeyProvider;
        private readonly TokenOptions tokenOptions;

        public JwtTokenProvider(ISecurityKeyProvider securityKeyProvider, ConfigOptions<TokenOptions> tokenConfig)
        {
            this.securityKeyProvider = securityKeyProvider;
            tokenOptions = tokenConfig.Options;
            BearerOptions = new JwtBearerOptions
            {
                TokenValidationParameters = CreateTokenValidationParameters(),
                RequireHttpsMetadata = false
            };
        }

        public JwtBearerOptions BearerOptions { get; private set; }

        public DateTime Issied { get; private set; }

        public DateTime Expires { get; private set; }

        public virtual string GenerateToken(string user, int id, string[] roles)
        {
            var tokenIdentity = new JwtTokenIdentity(user, id, roles);
            return NewToken(tokenIdentity);
        }

        private string NewToken(JwtTokenIdentity tokenIdentity)
        {
            Issied = DateTime.UtcNow;

            var handler = new JwtSecurityTokenHandler();

            Expires = Issied.AddMinutes(tokenOptions.Expiration);
            ClaimsIdentity identity = tokenIdentity.ToClaimsIdentity();
            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = BearerOptions.TokenValidationParameters.ValidIssuer,
                Audience = BearerOptions.TokenValidationParameters.ValidAudience,
                SigningCredentials = securityKeyProvider.Credentials,
                Subject = identity,
                Expires = Expires,
                IssuedAt = Issied
            };
            var securityToken = handler.CreateToken(descriptor);

            return handler.WriteToken(securityToken);
        }

        private TokenValidationParameters CreateTokenValidationParameters()
        {
            var validationParams = new TokenValidationParameters()
            {
                IssuerSigningKey = securityKeyProvider.Key,
                ValidAudience = tokenOptions.Audience,
                ValidIssuer = tokenOptions.Issuer,
                LifetimeValidator = ValidateTokenLifetime,
                RequireExpirationTime = true,
                AuthenticationType = JwtTokenConstants.AuthenticationType,
            };           

            return validationParams;
        }

        private bool ValidateTokenLifetime(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters valdationParams)
        {
            var now = DateTime.Now.ToUniversalTime();
            return now >= notBefore.Value && now <= expires.Value;
        }       
    }
}
