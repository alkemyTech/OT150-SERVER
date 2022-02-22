using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Interfaces
{
    public interface ITokenParameter
    {
        int Id { get; set; }
        [EmailAddress]
        string Email { get; set; }
        string Password { get; set; }
        string Role { get; set; }
    }
}
