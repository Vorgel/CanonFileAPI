namespace CanonFileAPI.Interfaces
{
    public interface IFileService
    {
        IList<IFile> GetFiles();
        void AddFile(IFile file);
    }
}
