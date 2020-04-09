namespace TODOListMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserModels", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        IsEmailVerified = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskModels", "UserId", "dbo.UserModels");
            DropIndex("dbo.TaskModels", new[] { "UserId" });
            DropTable("dbo.UserModels");
            DropTable("dbo.TaskModels");
        }
    }
}
