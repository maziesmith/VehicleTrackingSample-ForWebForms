using System;
using System.Configuration;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public static class ProviderFactory
    {
        public static TrackingDataProvider CreateDataProvider()
        {
            TrackingDataProvider provider = null;

            bool isAccessDatabase = false;
            string connectionString = String.Empty;
            DatabaseType databaseType = DatabaseType.AccessDatabase;
            if (bool.TryParse(ConfigurationManager.AppSettings["UseAccess"].ToLower(), out isAccessDatabase))
            {
                if (isAccessDatabase)
                {
                    connectionString = ConfigurationManager.AppSettings["AccessDataBase"];
                }
                else
                {
                    databaseType = DatabaseType.SqlServerDatabase;
                    connectionString = ConfigurationManager.AppSettings["VehicleTrackingDbConnectionString"];
                }
            }
            else
            {
                throw new Exception("You haven't specified the UseAccess value!");
            }

            switch (databaseType)
            { 
                case DatabaseType.AccessDatabase:
                    provider = new TrackingAccessProvider(System.Web.HttpContext.Current.Server.MapPath(connectionString));
                    break;
                default:
                    break;
            }

            return provider;
        }
    }
}