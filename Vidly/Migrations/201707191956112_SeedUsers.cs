namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7f060f48-2388-4972-b722-3f4ccba6c987', N'guest@vidly.com', 0, N'AIDnhucZHsuQZ6O3/kGysKc49SnPo606soDkQjqKSHLJ6BOJRze0dOIl2iOmfn1mzA==', N'f2535a43-b755-43b4-a124-d24de481075a', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd0ed97f7-6053-493e-8d05-75158643751c', N'admin@vidly.com', 0, N'ACTeX/F16QSp7iag04Ale8TBcJ11zAHHOFLTY03IblvplyAqbnYaNcQjUj7G9asajw==', N'49219d9a-bb22-4121-8cd5-55665dc78e05', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'df7d20d5-f698-475d-a61f-8c8ab1593c50', N'CanManageMovies')
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd0ed97f7-6053-493e-8d05-75158643751c', N'df7d20d5-f698-475d-a61f-8c8ab1593c50')");
        }
        
        public override void Down()
        {
        }
    }
}
