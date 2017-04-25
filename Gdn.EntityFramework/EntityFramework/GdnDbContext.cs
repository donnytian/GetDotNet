using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Abp.Zero.EntityFramework;
using Gdn.Authorization;
using Gdn.Authorization.Roles;
using Gdn.MultiTenancy;
using Gdn.Users;

namespace Gdn.EntityFramework
{
    public class GdnDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        public virtual IDbSet<Token> Tokens { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Token>()
                .Property(t => t.ClientId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }

        #region Constructors

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public GdnDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in GdnDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of GdnDbContext since ABP automatically handles it.
         */
        public GdnDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public GdnDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public GdnDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        #endregion
    }
}
