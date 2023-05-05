

using System.ComponentModel.DataAnnotations;

namespace TransportApp.Domain;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public Role Role { get; set; }
    public int RoleId { get; set; }
}