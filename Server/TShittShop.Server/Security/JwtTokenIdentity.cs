using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using TShirtShop.Shared;

namespace TShirtShop.Server.Security
{
    public class JwtTokenIdentity : IAppIdentity
    {
        public JwtTokenIdentity(string userName, int id, string[] roles)
        {
            Name = userName;
            Id = id;
            Roles = roles;
        }

        public JwtTokenIdentity(ClaimsIdentity identity)
        {
            Name = identity.Name;

            int.TryParse(identity.FindFirst(JwtTokenConstants.ClaimId)?.Value, out int tmp);
            Id = tmp;

            Roles = identity.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string[] Roles { get; private set; }

        public string AuthenticationType
        {
            get
            {
                return JwtTokenConstants.AuthenticationType;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return Id > 0;
            }
        }

        public bool IsInRole(string role)
        {
            return Roles != null && Roles.Contains(role);
        }

        public ClaimsIdentity ToClaimsIdentity()
        {
            var jti = Guid.NewGuid().ToString();
            var id = Id.ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtTokenConstants.ClaimId, id, ClaimValueTypes.Integer)
            };

            foreach (var userRole in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return new ClaimsIdentity(new GenericIdentity(Name, AuthenticationType), claims);
        }
    }
}
