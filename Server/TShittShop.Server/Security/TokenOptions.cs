namespace TShirtShop.Server.Security
{
    public class TokenOptions
    {
        public static string Section => "Token";

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int Expiration { get; set; }
    }
}
