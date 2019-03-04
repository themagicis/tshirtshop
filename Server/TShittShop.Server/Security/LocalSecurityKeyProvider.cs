using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TShirtShop.Server.Security
{
    public class LocalSecurityKeyProvider : ISecurityKeyProvider
    {
        private const string SecretKey = "needtogetthisfromenvironment";       
        
        private SymmetricSecurityKey securityKey;

        private SigningCredentials credentials;

        public SecurityKey Key
        {
            get
            {
                if (securityKey == null)
                {
                    InitializeKey();
                }

                return securityKey;
            }
        }

        public SigningCredentials Credentials
        {
            get
            {
                if (credentials == null)
                {
                    InitializeCredentials();
                }

                return credentials;
            }
        }

        private void InitializeCredentials()
        {
            credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }

        private void InitializeKey()
        {
            securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }
    }
}
