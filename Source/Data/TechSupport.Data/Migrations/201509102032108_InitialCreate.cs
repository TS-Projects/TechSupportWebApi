namespace TechSupport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CustomerAnswers", "IsDeleted");
            DropColumn("dbo.CustomerAnswers", "DeletedOn");
            DropColumn("dbo.CustomerAnswers", "IsHidden");
            DropColumn("dbo.Customers", "IsDeleted");
            DropColumn("dbo.Customers", "DeletedOn");
            DropColumn("dbo.Customers", "IsHidden");
            DropColumn("dbo.CustomerCards", "IsDeleted");
            DropColumn("dbo.CustomerCards", "DeletedOn");
            DropColumn("dbo.CustomerCards", "IsHidden");
            DropColumn("dbo.CustomerCardCategories", "IsDeleted");
            DropColumn("dbo.CustomerCardCategories", "DeletedOn");
            DropColumn("dbo.CustomerCardCategories", "IsHidden");
            DropColumn("dbo.CustomerCardQuestions", "IsDeleted");
            DropColumn("dbo.CustomerCardQuestions", "DeletedOn");
            DropColumn("dbo.CustomerCardQuestions", "IsHidden");
            DropColumn("dbo.CustomerCardQuestionAnswers", "IsDeleted");
            DropColumn("dbo.CustomerCardQuestionAnswers", "DeletedOn");
            DropColumn("dbo.CustomerCardQuestionAnswers", "IsHidden");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerCardQuestionAnswers", "IsHidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerCardQuestionAnswers", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.CustomerCardQuestionAnswers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerCardQuestions", "IsHidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerCardQuestions", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.CustomerCardQuestions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerCardCategories", "IsHidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerCardCategories", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.CustomerCardCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerCards", "IsHidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerCards", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.CustomerCards", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "IsHidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Customers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerAnswers", "IsHidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerAnswers", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.CustomerAnswers", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
