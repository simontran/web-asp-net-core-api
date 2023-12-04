using FluentMigrator;

namespace WebApiRestful.Data.Migrations
{
    [Migration(20231204133000)]
    public class InitialTables_20231204133000 : Migration
    {
        public override void Down()
        {
            Delete.Table("Todo");
        }
        public override void Up()
        {
            Create.Table("Todo")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("IsComplete").AsInt16().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().NotNullable()
                .WithColumn("ModifiedUser").AsString(50).NotNullable()
                .WithColumn("ModifiedDate").AsDateTime().NotNullable();
        }
    }
}
