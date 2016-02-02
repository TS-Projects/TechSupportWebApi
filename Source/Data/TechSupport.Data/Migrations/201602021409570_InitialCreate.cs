namespace TechSupport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAnswers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        CustomerCardQuestionId = c.Int(nullable: false),
                        Answer = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.CustomerCardQuestionId })
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.CustomerCardQuestions", t => t.CustomerCardQuestionId)
                .Index(t => t.CustomerId)
                .Index(t => t.CustomerCardQuestionId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsOfficial = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomerCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsVisible = c.Boolean(nullable: false),
                        CategoryId = c.Int(),
                        CustomerFirstName = c.String(nullable: false, maxLength: 100),
                        CustomerLastName = c.String(nullable: false, maxLength: 100),
                        CustomerAddress = c.String(maxLength: 100),
                        CustomerPhone = c.String(maxLength: 100),
                        Description = c.String(),
                        Comment = c.String(),
                        Summary = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnrollmentDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Informed = c.Boolean(nullable: false),
                        Warranty = c.Boolean(nullable: false),
                        CustomerCardPassword = c.String(maxLength: 20),
                        CustomerId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerCardCategories", t => t.CategoryId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CategoryId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerCardCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(),
                        IsVisible = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerCardCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.CustomerCardQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerCardId = c.Int(nullable: false),
                        Text = c.String(),
                        AskOfficialCustomers = c.Boolean(nullable: false),
                        AskPracticeCustomers = c.Boolean(nullable: false),
                        RegularExpressionValidation = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerCards", t => t.CustomerCardId)
                .Index(t => t.CustomerCardId);
            
            CreateTable(
                "dbo.CustomerCardQuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Text = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerCardQuestions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        City = c.String(maxLength: 100),
                        Address = c.String(maxLength: 100),
                        CreatedOn = c.DateTime(nullable: false),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsHidden = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CustomerAnswers", "CustomerCardQuestionId", "dbo.CustomerCardQuestions");
            DropForeignKey("dbo.CustomerAnswers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerCardQuestions", "CustomerCardId", "dbo.CustomerCards");
            DropForeignKey("dbo.CustomerCardQuestionAnswers", "QuestionId", "dbo.CustomerCardQuestions");
            DropForeignKey("dbo.CustomerCards", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerCards", "CategoryId", "dbo.CustomerCardCategories");
            DropForeignKey("dbo.CustomerCardCategories", "ParentId", "dbo.CustomerCardCategories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CustomerCardQuestionAnswers", new[] { "QuestionId" });
            DropIndex("dbo.CustomerCardQuestions", new[] { "CustomerCardId" });
            DropIndex("dbo.CustomerCardCategories", new[] { "ParentId" });
            DropIndex("dbo.CustomerCards", new[] { "CustomerId" });
            DropIndex("dbo.CustomerCards", new[] { "CategoryId" });
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.CustomerAnswers", new[] { "CustomerCardQuestionId" });
            DropIndex("dbo.CustomerAnswers", new[] { "CustomerId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CustomerCardQuestionAnswers");
            DropTable("dbo.CustomerCardQuestions");
            DropTable("dbo.CustomerCardCategories");
            DropTable("dbo.CustomerCards");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerAnswers");
        }
    }
}
