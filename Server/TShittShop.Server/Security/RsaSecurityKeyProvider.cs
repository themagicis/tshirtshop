using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Xml;

namespace TShirtShop.Server.Security
{
    public class RsaSecurityKeyProvider : ISecurityKeyProvider
    {
        private RsaSecurityKey securityKey;

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
            credentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }

        private void InitializeKey()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                LoadXmlString(rsa, @"<RSAKeyValue><Modulus>RnUZUvrekimRHSb2MylS5W5wDigsnAIbmz3j2T07+fA8fa6DFG+UrjZGZ+KhcqkpIVNwjjhtzH/iF6xe2tOKEXFX4V5GKiHpm+D2gdr4kn6hVPCiQilxuO3EYmbla/++XqAuCUhzcyVUXLoNs3nTbvrzpCbMo9KzBrUczxwIf0MbG0IMU5X8n44kAEp4ykhWBoRKcRax1XQjP4mOy14iWMFA5eV0/WKy++wqpSMfs/uATkXVsX5N0NJXPJ2oMggOSqzwkvj8K5Dm4u18+azi61SXpSc6AfikstUIGaL+EDUAV6zq86sRm974nD+H5Gp/3EhfxBZ7M5RvEwWfPv08aQ==</Modulus><Exponent>AQAB</Exponent><P>hnBq18Fu8p9/J2U4Ah9xIZBWpTyHfmHabYm/PqbCbMUX7JVGgSFAd3ebJn7rpdT+V9UmN+bUjpXT9bQvdrG887bkVxi+2/THG6k4Or4Py+QbYpsxR3I0g9YpVKJMNgp6im72AUqQyFgWPt6oSfoOVAwo3bcClljy1e8S/3gplLk=</P><Q>hiphB3dTd0+DxSFoIol82eg3ynt8tULm+LrtczfRkNFtF6cyih3jYC3bR9vFzKeKPNF6pVoRQxEFAuPqL6yPSGW+kNYgCnNSOLci+aV9cMC9kDd7x9RPY4Se4RxFcLZ5hjcmxK3hMfa4ZM+NxCD4lAihrhoa5q4hvgGEhd/dbTE=</Q><DP>HKOMDyQ0x1i01KaaVNFv3y+JVa/cSU79IgKLDASoPFWMMCjomQ+FVS3UjlHRT0VdXUaZ2qTAuRMv0RKuXcGlN+HJKMirt92gIMNeqBze7pdMQig/SjII/+C6cz4TsBtxWVYj78h7qUIikwgJn6C2Nq9UcU7QLUkLiglg6a0mZnk=</DP><DQ>LxCCxSm43+vG38peS29Lmu15VNCI6NU1TR/V2EtGerTcviVq90SQ1NaBS+3ur2I+fiNGEOdNkudPiLoHFv5DrTPz678RsljOCRNFc6n4HSuf1A2CoJD74H9PdMyEF4wLiWejc+9DRlG0Ubj1lE20pcl5XifONAn10D+cC5KCjKE=</DQ><InverseQ>duQrsJjDun9P5vGNeu1W4e9eP7mz0DkXh9ERAoEJpFZ3h9PyvgCZzJkpFGqJmt0FxvTvsougVVAcvZ243qgKt/Tpvw6YrTvxPhZjDWRHGPLsRsaVHxa2ER9fQ5CXT3omw9r3WkJ3SI42nnWHFjvikZiOq7lHeH361hMzM7tFfPk=</InverseQ><D>J5oB1eYz26uJaP1RPIhpk5NzYEGscTOuGJ+8xYnpgB5LocjI3F+rDhBrmlCDtlLmT1j61rVY4ayvg+bamdx0qkEux/mZLm1JRNg5NdvCA3UxcPCgAtgQl4Ts8PIs2XsxoTYfKhCS85FzzXq14L8E3EgpUYaNVzdCcO6zdqWnEQAr7oB98aohtSFK2ROnneePJmOdwJBoZS7RkZ86fc1Qw+vfnMrW1Ju35OzgHGewmI37bosHKSJMB7umOooIDN5w0HfLS5Ft0iQPMVzPd7HqX4t3pOYNxrdjBW6PfahNjrpM/1yprJr2QdaAJhslXNChqvtKejMBijmDDeMco/iaAQ==</D></RSAKeyValue>");
                securityKey = new RsaSecurityKey(rsa.ExportParameters(true));
            }
        }

        private void LoadXmlString(RSA rsa, string xmlString)
        {
            var parameters = new RSAParameters();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus":
                            parameters.Modulus = Convert.FromBase64String(node.InnerText);
                            break;
                        case "Exponent":
                            parameters.Exponent = Convert.FromBase64String(node.InnerText);
                            break;
                        case "P":
                            parameters.P = Convert.FromBase64String(node.InnerText);
                            break;
                        case "Q":
                            parameters.Q = Convert.FromBase64String(node.InnerText);
                            break;
                        case "DP":
                            parameters.DP = Convert.FromBase64String(node.InnerText);
                            break;
                        case "DQ":
                            parameters.DQ = Convert.FromBase64String(node.InnerText);
                            break;
                        case "InverseQ":
                            parameters.InverseQ = Convert.FromBase64String(node.InnerText);
                            break;
                        case "D":
                            parameters.D = Convert.FromBase64String(node.InnerText);
                            break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }
    }
}
