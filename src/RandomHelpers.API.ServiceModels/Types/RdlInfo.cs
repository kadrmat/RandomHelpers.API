namespace RandomHelpers.API.ServiceModels.Types
{
    public class RdlInfo
    {
        public string RdlFolderPath { get; set; }
        public bool SaveToDisk { get; set; }
        public string HtmlSavePath { get; set; } = string.Empty;
    }
}
