using System;
using System.Configuration;

namespace progforreznik
{

    class ConfigCrypt
    {
        protected string connectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            }
        }

        protected void ConfigConnectionStringCrypt()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("connectionString", @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True; Pooling=true;"));
            config.Save();

            ConnectionStringsSection section = config.GetSection("progforreznik.Properties.Settings.DatabaseConnectionString") as ConnectionStringsSection;

            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            }

            config.Save();

        }
    }
}
