namespace webapiG2T.Services.Interfaces
{
    public interface IRevoquerTokenService
    {
        Task AddToken(string Id, string token, DateTime expire);
        Task RevoquerTokenAsync(string token);
        Task<bool> EstRevoquerTokenAsync(string token);
    }
}
