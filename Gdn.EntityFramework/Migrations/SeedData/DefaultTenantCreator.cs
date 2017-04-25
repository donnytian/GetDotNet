using System.Linq;
using Gdn.EntityFramework;
using Gdn.MultiTenancy;

namespace Gdn.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly GdnDbContext _context;

        public DefaultTenantCreator(GdnDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
