namespace EntityManuelMigration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Uruns", "Kategori_KategoriID", "dbo.Kategoris");
            DropIndex("dbo.Uruns", new[] { "Kategori_KategoriID" });
            RenameColumn(table: "dbo.Uruns", name: "Kategori_KategoriID", newName: "KategoriID");
            AddColumn("dbo.Uruns", "Picture", c => c.Binary());
            AlterColumn("dbo.Uruns", "KategoriID", c => c.Int(nullable: false));
            CreateIndex("dbo.Uruns", "KategoriID");
            AddForeignKey("dbo.Uruns", "KategoriID", "dbo.Kategoris", "KategoriID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uruns", "KategoriID", "dbo.Kategoris");
            DropIndex("dbo.Uruns", new[] { "KategoriID" });
            AlterColumn("dbo.Uruns", "KategoriID", c => c.Int());
            DropColumn("dbo.Uruns", "Picture");
            RenameColumn(table: "dbo.Uruns", name: "KategoriID", newName: "Kategori_KategoriID");
            CreateIndex("dbo.Uruns", "Kategori_KategoriID");
            AddForeignKey("dbo.Uruns", "Kategori_KategoriID", "dbo.Kategoris", "KategoriID");
        }
    }
}
