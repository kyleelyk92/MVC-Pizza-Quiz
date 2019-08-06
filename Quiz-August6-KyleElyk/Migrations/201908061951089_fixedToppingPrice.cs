namespace Quiz_August6_KyleElyk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedToppingPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Toppings", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Toppings", "Price", c => c.Int(nullable: false));
        }
    }
}
