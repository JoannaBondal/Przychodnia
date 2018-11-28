namespace Przychodnia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wizyta_DeleteGodzina : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Wizytas", "Godzina");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wizytas", "Godzina", c => c.DateTime(nullable: false));
        }
    }
}
