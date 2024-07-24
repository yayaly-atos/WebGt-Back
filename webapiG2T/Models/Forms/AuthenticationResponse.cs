using System.IdentityModel.Tokens.Jwt;

namespace webapiG2T.Models.Forms
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string? token;
        public DateTime expiration;
    }
}
