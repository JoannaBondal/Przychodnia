namespace Przychodnia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wizyta_RodzajWizyty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wizytas", "RodzajWizyty", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wizytas", "RodzajWizyty");
        }
    }
}
