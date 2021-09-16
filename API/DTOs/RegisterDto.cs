using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]  public string FullName { get; set; }
        [Required]  public string Email { get; set; }
        [Required]  public string PhoneNumber { get; set; }
        [Required]  public string Password { get; set; }

        public string UserName
        {
            get { 
                var fullNameParts = FullName.ToLower().Split(' ');
                var userName = "";
                foreach (var part in fullNameParts){
                    userName+=part;
                }
                return userName;
            }
        }

        public string NormalizedUsername{
            get{
                return UserName.ToUpper();
            }
        }

        public string NormalizedEmail{
            get{
                return Email.ToUpper();
            }
        }
    }
}