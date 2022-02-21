using OngProject.Entities.Jwt;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface ITokenRepository
    {

 
        public Task<RequestToken> GetToken(IdentityUserEntity identityUser);

    }
}
