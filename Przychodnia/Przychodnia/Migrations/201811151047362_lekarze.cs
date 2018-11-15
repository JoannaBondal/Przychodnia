namespace Przychodnia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lekarze : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lekarzs",
                c => new
                    {
                        ID_Lekarza = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        Telefon = c.Int(nullable: false),
                        Osoba_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID_Lekarza)
                .ForeignKey("dbo.AspNetUsers", t => t.Osoba_Id)
                .Index(t => t.Osoba_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lekarzs", "Osoba_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Lekarzs", new[] { "Osoba_Id" });
            DropTable("dbo.Lekarzs");
        }
    }
}
