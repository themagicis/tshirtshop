namespace App.Common.Config
{
    public class ConnectionStringOptions
    {
        public static string Section => "ConnectionStrings";

        public string DefaultConnection { get; set; }
    }
}
