namespace Pub.API.Service.Ftp {
    public interface IFtpService {
        Task<bool> CreateFtpFolderAsync(string? path);

        Task<string> ListDirectoryAsync(string? path);

        Task UploadFileAsync(string fileData, string destinationPath, string fileName);

        string DownloadFile(string filePath, string fileName);
    }
}
