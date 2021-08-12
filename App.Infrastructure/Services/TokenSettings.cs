using App.Shared;

namespace App.Infrastructure.Services
{
    public class TokenSettings
    {
        //For use JWT Authentication
        public static string SECRET_KEY = Runtime.JwtSecretKey;
        public static string AUDIENCE = Runtime.Audience;
        public static string ISSUER = Runtime.Issuer;
        public static int HOUR_VALIDATION_TOKEN = Runtime.ExpiresInHour;
    }
}
