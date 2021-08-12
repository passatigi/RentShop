using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]  public string FullName { get; set; }
        [Required]  public string Email { get; set; }
        [Required]  public string PhoneNumber { get; set; }
        [Required]  public string Password { get; set; }
    }
}