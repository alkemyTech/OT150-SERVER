using OngProject.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace OngProject.Core.Helper
{
    public class EncryptHelper:IEncryptHelper
    {
     
     

        string IEncryptHelper.EncryptPassSha256(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
