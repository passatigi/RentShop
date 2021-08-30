namespace API.DTOs
{
    public class UserUpdateDto
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

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