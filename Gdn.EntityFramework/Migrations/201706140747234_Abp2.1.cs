namespace Gdn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Abp21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpLanguages", "IsDisabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpLanguages", "IsDisabled");
        }
    }
}
