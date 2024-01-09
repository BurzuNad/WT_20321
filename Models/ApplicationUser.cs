using Microsoft.AspNetCore.Identity;

namespace VT_20321.Models {
    public class ApplicationUser: IdentityUser {
        public byte[]? Avatar { get; set; }
    }
}
