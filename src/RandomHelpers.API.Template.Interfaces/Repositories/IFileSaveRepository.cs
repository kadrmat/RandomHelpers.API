namespace RandomHelpers.API.Template.Interfaces.Repositories
{
    public interface IFileSaveRepository
    {
        void SaveFileToDisk(byte[] file, string filePath, string fileName, bool isException);
    }
}
