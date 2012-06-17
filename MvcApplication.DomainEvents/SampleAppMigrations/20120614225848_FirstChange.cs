namespace SampleAppMigrations
{
  using System;
  using System.ComponentModel;
  using Mindscape.NHibernateModelDesigner.Migrations;
  
  
  [Migration("20120614225848")]
  public class FirstChange : Migration
  {
    
    public override void Up()
    {
      this.AddKeyTable("KeyTable", null, ModelDataType.Int32, 1);
      this.AddTable("Forum", null, new Field("Id", ModelDataType.Int32, false).AsIdentityColumn(), new Field[] {
            new Field("Name", ModelDataType.String, false)});
      this.AddTable("Tasks", null, new Field("Id", ModelDataType.Int32, false).AsIdentityColumn(), new Field[] {
            new Field("Name", ModelDataType.String, false),
            new ForeignKeyField("ForumId", ModelDataType.Int32, true, "Forum", null, "Id")});
    }
    
    public override void Down()
    {
      this.DropColumn("Tasks", null, "ForumId", true);
      this.DropTable("Tasks", null);
      this.DropTable("Forum", null);
    }
  }
}
