namespace Gdn.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeChatAccount : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tokens", new[] { "ClientId" });
            CreateTable(
                "dbo.WeChatAccounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 64),
                        TenantId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        AccountType = c.Int(nullable: false),
                        AppId = c.String(maxLength: 64),
                        AppSecret = c.String(maxLength: 64),
                        Url = c.String(),
                        Token = c.String(maxLength: 64),
                        EncodingAesKey = c.String(maxLength: 64),
                        SecurityMode = c.Int(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WeChatAccount_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WeChatAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            DropTable("dbo.Tokens",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Token_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.WeChatAccounts", new[] { "Name" });
            DropTable("dbo.WeChatAccounts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WeChatAccount_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WeChatAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            CreateIndex("dbo.Tokens", "ClientId", unique: true);
        }
    }
}
