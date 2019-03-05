namespace App.Email
{
    public class SendGridOptions
    {
        public static string Section => "Token";

        public string ApiKey { get; set; }

        public string Emails { get; set; }
    }
}
