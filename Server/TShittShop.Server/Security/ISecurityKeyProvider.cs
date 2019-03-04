using Microsoft.IdentityModel.Tokens;

namespace TShirtShop.Server.Security
{
    public interface ISecurityKeyProvider
    {
        SigningCredentials Credentials { get; }

        SecurityKey Key { get; }
    }
}
