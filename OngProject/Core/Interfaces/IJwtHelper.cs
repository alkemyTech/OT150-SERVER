namespace OngProject.Core.Interfaces
{
    public interface IJwtHelper
    {
        public string GenerateJwtToken(ITokenParameter tokenParameter);
    }
}
