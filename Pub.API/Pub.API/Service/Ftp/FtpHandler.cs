﻿using System.Net;
using System.Net.Security;

namespace Pub.API.Service.Ftp {
    public class FtpHandler : IFtpService {

        private string? host;
        private string? user;
        private string? password;

        public FtpHandler(IConfiguration configuration)
        {
            host = configuration["Ftp:Host"];
            user = configuration["Ftp:User"];
            password = configuration["Ftp:Password"];
        }

        public bool DoesFtpDirectoryExist(string path) {
            bool isexist = false;

            try {

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + host + "/" + path);
                request.Credentials = new NetworkCredential(user, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
                    isexist = true;
                }
            } catch (WebException ex) {
                if (ex.Response != null) {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable) {
                        return false;
                    }
                }
            }
            return isexist;
        }


        public async Task<bool> CreateFtpFolderAsync(string? path) {
            Uri uri=new Uri("ftp://"+host+"/"+path);
            WebRequest request = WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(user, password);
            var result=(FtpWebResponse)await request.GetResponseAsync();
            result.Close();
            return true;
            
        }

        public async Task<string> ListDirectoryAsync(string? path) {
            Uri uri = new Uri("ftp://" +host+ path);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials= new NetworkCredential(user, password);
            request.UseBinary = false;
            var result = (FtpWebResponse)await request.GetResponseAsync();
            var stream = result.GetResponseStream();
            StreamReader reader=new StreamReader(stream);
            var dirsData=await reader.ReadToEndAsync();
            result.Close();
            reader.Close();
            return dirsData;
        }

        public async Task UploadFileAsync(string fileData, string destinationPath,string fileName) {

            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://"+host+"/"+destinationPath+"/"+ fileName);
            req.UseBinary = true;
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential(user, password);
            req.ContentLength=fileData.Length;
            Stream reqStream = await req.GetRequestStreamAsync();
            byte[] decodedData=Convert.FromBase64String(fileData);
            await reqStream.WriteAsync(decodedData,0,decodedData.Length);
            reqStream.Close();

        }

        public string DownloadFile(string filePath, string fileName) {
            WebClient client = new WebClient() {
                Credentials = new NetworkCredential(user, password)
            };
            Uri uri = new Uri("ftp://"+host+"/"+filePath+"/"+fileName);
            byte[] fileData = client.DownloadData(uri);
            client.Dispose();
            return Convert.ToBase64String(fileData);
        }
    }
}
