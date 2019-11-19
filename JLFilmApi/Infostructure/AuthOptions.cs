using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JLFilmApi.Infostructure
{
    public class AuthOptions
    {
        public const string ISSUER = "JL_project";
        public const string AUDIENCE = "app_client";
        const string KEY = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
