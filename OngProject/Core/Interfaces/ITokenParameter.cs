using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Interfaces
{
    public interface ITokenParameter
    {
        int Id { get; set; }
        [EmailAddress]
        string Email { get; set; }
        string Password { get; set; }
        int RoleId { get; set; }
    }
}
