using System.Configuration;


namespace PortableDataAccessLayer
{
    internal static class Configuration
    {
        const string DEFAULT_CONNECTION_KEY = "defaultConnection";

        public static string DefaultConnection
        {
            get
            {
                return "";// ConfigurationManager.AppSettings[DEFAULT_CONNECTION_KEY];
            }
        }

        //public static string ProviderName
        //{
        //    get
        //    {
        //        return ConfigurationManager.ConnectionStrings[DefaultConnection].ProviderName;
        //    }
        //}

        private static string providerName;

        public static string ProviderName
        {
            get { return providerName; }
            set { providerName = value; }
        }



        //public static string ConnectionString
        //{
        //    get
        //    {
        //        return ConfigurationManager.ConnectionStrings[DefaultConnection].ConnectionString;
        //    }
        //}

        private static string connectionString;

        public static string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public static string GetConnectionString(string connectionName)
        {
            return ConnectionString;
        }

        public static string GetProviderName(string connectionName)
        {
            return ProviderName;
        }

    }
}
