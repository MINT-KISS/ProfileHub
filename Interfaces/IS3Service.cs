namespace ProfileHub.Interfaces
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(IFormFile file, string keyName);
    }
}
