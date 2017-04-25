using System.Threading.Tasks;
using Abp.Application.Services;
using Gdn.Roles.Dto;

namespace Gdn.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
