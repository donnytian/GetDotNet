using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Gdn.MultiTenancy.Dto;

namespace Gdn.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
