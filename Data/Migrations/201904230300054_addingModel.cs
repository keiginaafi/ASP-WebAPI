namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        DeleteDate = c.DateTimeOffset(nullable: false, precision: 7),
                        IsDelete = c.Boolean(nullable: false),
                        Suppliers_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.Suppliers_Id)
                .Index(t => t.Suppliers_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        DeleteDate = c.DateTimeOffset(nullable: false, precision: 7),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Suppliers_Id", "dbo.Suppliers");
            DropIndex("dbo.Items", new[] { "Suppliers_Id" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Items");
        }
    }
}
