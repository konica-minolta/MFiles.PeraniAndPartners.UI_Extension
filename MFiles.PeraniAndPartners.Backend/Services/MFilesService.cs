
using MFilesAPI;
using Microsoft.AspNetCore.Hosting.Server;

namespace MFiles.PeraniAndPartners.Backend.Services
{
    public class MFilesService
    {
       
        private static readonly Guid sampleVaultGuid  = Guid.Parse("{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}");

        public static string GetMail (string accountName, string guid,string username,string password)
        {
            MFilesServerApplication oServerApp = new MFilesAPI.MFilesServerApplication();
            MFServerConnection conn = oServerApp.Connect(MFilesAPI.MFAuthType.MFAuthTypeSpecificMFilesUser, username, password,"", "ncacn_ip_tcp", "localhost", "2266");
            LoginAccount account = oServerApp.LoginAccountOperations.GetLoginAccount(accountName);
            return account.EmailAddress;
        }

    }
}
