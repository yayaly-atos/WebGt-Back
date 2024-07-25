namespace webapiG2T.Models
{
    public class RevoquerToken
    {
        public string Id { get; set; }
        public string? Token { get; set; }
        public DateTime DateRevoquer { get; set; }
        public Boolean IsRevoquer { get; set; } = false;
    }
}
