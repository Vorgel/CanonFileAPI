using CanonFileAPI.Interfaces;

namespace CanonFileAPI.Services
{
    public class FilesService : IFileService
    {
        public IList<IFile> files;

        public FilesService()
        {
            this.files = new List<IFile>();
        }

        public IList<IFile> GetFiles()
        {
            return this.files;
        }

        public void AddFile(IFile file)
        {
            if (this.files.Any(x => x.Name == file.Name))
            {
                throw new ArgumentException("File already exists");
            }

            if (file.Name.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                throw new ArgumentException("Incorrect file name");
            }

            this.files.Add(file);
        }
    }
}
