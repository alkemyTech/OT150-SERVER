using OngProject.Core.Interfaces;

namespace OngProject.Core.Models
{
    public class TokenParameter : ITokenParameter
    {
        public int Id { get; set; }
        public string Email { get ; set; }
        public string Password { get ; set; }
        public string Role { get; set ; }
    }
}
