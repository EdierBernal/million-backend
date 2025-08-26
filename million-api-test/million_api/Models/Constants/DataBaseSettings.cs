namespace million_api.Models.Constants
{
    internal class DataBaseSettings
    {
        public const string OwnersCollection = "Owner";
        public const string PropertiesCollection = "Property";
        public const string PropertyTracesCollection = "PropertyTrace";
        public const string PropertyImagesCollection = "PropertyImage";

        public string ConnectionString { get; internal set; }
        public string DatabaseName { get; internal set; }
    }
}