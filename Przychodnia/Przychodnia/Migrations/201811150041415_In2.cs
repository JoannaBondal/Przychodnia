namespace Przychodnia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class In2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wizytas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Godzina = c.DateTime(nullable: false),
                        Lekarz_Id = c.String(maxLength: 128),
                        Pacjent_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Lekarz_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Pacjent_Id)
                .Index(t => t.Lekarz_Id)
                .Index(t => t.Pacjent_Id);
            
            AddColumn("dbo.AspNetUsers", "Test", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wizytas", "Pacjent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Wizytas", "Lekarz_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Wizytas", new[] { "Pacjent_Id" });
            DropIndex("dbo.Wizytas", new[] { "Lekarz_Id" });
            DropColumn("dbo.AspNetUsers", "Test");
            DropTable("dbo.Wizytas");
        }
    }
}
