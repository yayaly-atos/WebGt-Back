
using webapiG2T.Models.Forms;

namespace webapiG2T.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Response> CreateRole(RoleModel roleName);
    }
}
