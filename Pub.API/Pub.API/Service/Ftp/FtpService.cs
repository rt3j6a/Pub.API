using System.Net;

namespace Pub.API.Service.Ftp {
    public class FtpService {

        private string host;
        private string user;
        private string password;

        public FtpService(IConfiguration configuration)
        {
            host = configuration["FtpHost"];
            user = configuration["FtpUser"];
            password = configuration["FtpPassword"];
        }

        public async Task<bool> CreateFtpFolderAsync(string path) {
            try {
                WebRequest request = WebRequest.Create(host + path);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(user, password);
                var result=(FtpWebResponse)await request.GetResponseAsync();
                return result.StatusCode.HasFlag(HttpStatusCode.OK);
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> UploadFileAsync(byte[] fileData, string destinationPath) {
            try {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + destinationPath);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(user, password);
                request.ContentLength = fileData.Length;

                Stream requestStream = await request.GetRequestStreamAsync();
                await requestStream.WriteAsync(fileData, 0, fileData.Length);
                requestStream.Close();
                
                return true;
            }catch(Exception) { return false; }
        }

        public byte[]? DownloadFileAsync(string filePath) {
            try {
                WebClient request = new WebClient();
                request.Credentials= new NetworkCredential(user, password);
                byte[] data=request.DownloadData(filePath);
                return data;

            }catch(Exception) { return null; }
        }
    }
}
