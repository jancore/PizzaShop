namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_MIME : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pizzas", "MIMEType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pizzas", "MIMEType");
        }
    }
}
