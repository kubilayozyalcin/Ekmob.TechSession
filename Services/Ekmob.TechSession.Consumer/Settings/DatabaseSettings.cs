namespace Ekmob.TechSession.Consumer.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CustomerCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
