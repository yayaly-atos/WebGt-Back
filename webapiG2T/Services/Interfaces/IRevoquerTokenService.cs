namespace webapiG2T.Services.Interfaces
{
    public interface IRevoquerTokenService
    {
        Task RevoquerTokenAsync(string token);
        Task<bool> EstRevoquerTokenAsync(string token);
    }
}
