namespace OngProject.Core.Interfaces
{
    public interface IEncryptHelper
    {
        string EncryptPassSha256(string password);
    }
}
