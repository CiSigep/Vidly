namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres(GenreType) VALUES ('Comedy')");
            Sql("INSERT INTO Genres(GenreType) VALUES ('Action')");
            Sql("INSERT INTO Genres(GenreType) VALUES ('Family')");
            Sql("INSERT INTO Genres(GenreType) VALUES ('Romance')");
            Sql("INSERT INTO Genres(GenreType) VALUES ('Thriller')");
        }
        
        public override void Down()
        {
        }
    }
}
