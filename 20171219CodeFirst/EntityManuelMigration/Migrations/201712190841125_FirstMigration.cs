namespace EntityManuelMigration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoris",
                c => new
                    {
                        KategoriID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.KategoriID);
            
            CreateTable(
                "dbo.Uruns",
                c => new
                    {
                        UrunID = c.Int(nullable: false, identity: true),
                        UrunAdi = c.String(),
                        Kategori_KategoriID = c.Int(),
                    })
                .PrimaryKey(t => t.UrunID)
                .ForeignKey("dbo.Kategoris", t => t.Kategori_KategoriID)
                .Index(t => t.Kategori_KategoriID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uruns", "Kategori_KategoriID", "dbo.Kategoris");
            DropIndex("dbo.Uruns", new[] { "Kategori_KategoriID" });
            DropTable("dbo.Uruns");
            DropTable("dbo.Kategoris");
        }
    }
}
