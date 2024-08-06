using System.IdentityModel.Tokens.Jwt;

namespace webapiG2T.Models.Forms
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string? Token;
        public string UserId { get; set; }
        public DateTime Expiration;
        public int? EntiteId;
    }
}
