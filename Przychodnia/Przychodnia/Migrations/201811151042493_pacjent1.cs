namespace Przychodnia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pacjent1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pacjents",
                c => new
                    {
                        ID_Pacjenta = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        Pesel = c.Int(nullable: false),
                        Adres = c.String(),
                        Plec = c.String(),
                        Telefon = c.Int(nullable: false),
                        Data_ur = c.DateTime(nullable: false),
                        Osoba_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID_Pacjenta)
                .ForeignKey("dbo.AspNetUsers", t => t.Osoba_Id)
                .Index(t => t.Osoba_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pacjents", "Osoba_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Pacjents", new[] { "Osoba_Id" });
            DropTable("dbo.Pacjents");
        }
    }
}
