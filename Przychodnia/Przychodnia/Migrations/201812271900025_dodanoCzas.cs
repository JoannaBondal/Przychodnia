namespace Przychodnia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanoCzas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wizytas", "Czas", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wizytas", "Czas");
        }
    }
}
