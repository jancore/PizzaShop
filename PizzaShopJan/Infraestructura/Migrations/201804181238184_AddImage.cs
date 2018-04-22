namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pizzas", "File", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pizzas", "File");
        }
    }
}
