namespace Gdn.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddToken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        ClientName = c.String(),
                        ClientId = c.String(nullable: false, maxLength: 512),
                        AccessToken = c.String(maxLength: 1024),
                        AccessTokenOld = c.String(maxLength: 1024),
                        RefreshToken = c.String(maxLength: 1024),
                        ExpiresAtUtc = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Token_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ClientId, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tokens", new[] { "ClientId" });
            DropTable("dbo.Tokens",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Token_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
