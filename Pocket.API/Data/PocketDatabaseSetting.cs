namespace Pocket.API.Data
{
    public class PocketDatabaseSetting
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PocketProfileCollectionName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string SocialProfileCollectionName { get; set; } = null!;  
    }
}
