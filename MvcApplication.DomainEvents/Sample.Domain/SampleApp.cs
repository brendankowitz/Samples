using System;
using System.Collections.Generic;
using NHibernate.Cfg;

namespace Sample.Domain
{

  /// <summary>
  /// Provides a NHibernate configuration object containing mappings for this model.
  /// </summary>
  public static class ConfigurationHelper
  {
    /// <summary>
    /// Creates a NHibernate configuration object containing mappings for this model.
    /// </summary>
    /// <returns>A NHibernate configuration object containing mappings for this model.</returns>
    public static Configuration CreateConfiguration()
    {
      var configuration = new Configuration();
      configuration.Configure();
      ApplyConfiguration(configuration);
      return configuration;
    }

    /// <summary>
    /// Adds mappings for this model to a NHibernate configuration object.
    /// </summary>
    /// <param name="configuration">A NHibernate configuration object to which to add mappings for this model.</param>
    public static void ApplyConfiguration(Configuration configuration)
    {
      configuration.AddXml(ModelMappingXml.ToString());
      configuration.AddAssembly(typeof(ConfigurationHelper).Assembly);
    }

    /// <summary>
    /// Provides global mappings not associated with a specific entity.
    /// </summary>
    public static System.Xml.Linq.XDocument ModelMappingXml
    {
      get
      {
        var mappingDocument = System.Xml.Linq.XDocument.Parse(@"<?xml version='1.0' encoding='utf-8' ?>
<hibernate-mapping xmlns='urn:nhibernate-mapping-2.2'
                   assembly='" + typeof(ConfigurationHelper).Assembly.GetName().Name + @"'
                   namespace='Sample.Domain'
                   default-cascade='save-update'
                   >
</hibernate-mapping>");
        return mappingDocument;
      }
    }
  }
}
