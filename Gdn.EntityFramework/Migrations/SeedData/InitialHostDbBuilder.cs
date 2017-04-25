using Gdn.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Gdn.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly GdnDbContext _context;

        public InitialHostDbBuilder(GdnDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
