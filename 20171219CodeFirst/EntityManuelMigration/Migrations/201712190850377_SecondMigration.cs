namespace EntityManuelMigration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kategoris", "turu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kategoris", "turu");
        }
    }
}
