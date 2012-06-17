using System;
using System.Collections.Generic;
using NHibernate.Cfg;

namespace Sample.Domain
{
    [System.CodeDom.Compiler.GeneratedCode("NHibernateModelGenerator", "1.0.0.0")]
    public partial class Forum
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        private IList<Tasks> _tasks = new List<Tasks>();

        public virtual IList<Tasks> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }

        public class ForumMap : FluentNHibernate.Mapping.ClassMap<Forum>
        {
            public ForumMap()
            {
                Table("`Forum`");
                DynamicUpdate();
                Id(x => x.Id, "`Id`")
                  .GeneratedBy
                    .Identity();
                Map(x => x.Name, "`Name`");
                HasMany(x => x.Tasks).Inverse()
                  .KeyColumn("`ForumId`")
                  .AsBag()
                  .Cascade.SaveUpdate();
            }
        }
    }


}
