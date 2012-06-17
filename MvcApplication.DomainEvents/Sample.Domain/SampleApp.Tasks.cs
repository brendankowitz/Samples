using System;
using System.Collections.Generic;
using NHibernate.Cfg;

namespace Sample.Domain
{
  [System.CodeDom.Compiler.GeneratedCode("NHibernateModelGenerator", "1.0.0.0")]
  public partial class Tasks
  {
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }

    public virtual Forum Forum { get; set; }

    public class TasksMap : FluentNHibernate.Mapping.ClassMap<Tasks>
    {
      public TasksMap()
      {
        Table("`Tasks`");
        Id(x => x.Id, "`Id`")
          .GeneratedBy
            .Identity();
        Map(x => x.Name, "`Name`")
;
        References(x => x.Forum)
          .Column("`ForumId`");
      }
    }
  }


}
