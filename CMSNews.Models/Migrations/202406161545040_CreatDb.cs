namespace CMSNews.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false, maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 30),
                        Registerdate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.T_News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.T_News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        NewsTitle = c.String(nullable: false, maxLength: 300),
                        Description = c.String(nullable: false),
                        ImageName = c.String(nullable: false, maxLength: 100),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        See = c.Int(nullable: false),
                        Like = c.Int(nullable: false),
                        NewsGroupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        NewsGroup_NewGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.T_NewGroups", t => t.NewsGroup_NewGroupId)
                .ForeignKey("dbo.T_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NewsGroup_NewGroupId);
            
            CreateTable(
                "dbo.T_NewGroups",
                c => new
                    {
                        NewGroupId = c.Int(nullable: false),
                        NewGroupTitle = c.String(nullable: false, maxLength: 200),
                        ImageName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.NewGroupId);
            
            CreateTable(
                "dbo.T_Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        MobileNumber = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 100),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Comments", "NewsId", "dbo.T_News");
            DropForeignKey("dbo.T_News", "UserId", "dbo.T_Users");
            DropForeignKey("dbo.T_News", "NewsGroup_NewGroupId", "dbo.T_NewGroups");
            DropIndex("dbo.T_News", new[] { "NewsGroup_NewGroupId" });
            DropIndex("dbo.T_News", new[] { "UserId" });
            DropIndex("dbo.T_Comments", new[] { "NewsId" });
            DropTable("dbo.T_Users");
            DropTable("dbo.T_NewGroups");
            DropTable("dbo.T_News");
            DropTable("dbo.T_Comments");
        }
    }
}
