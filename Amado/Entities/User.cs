using Microsoft.AspNetCore.Identity;

namespace Amado.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
