using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Another.Api.Dtos
{
    public class AccountDto
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenDto UserToken { get; set; }
    }

    public class UserToRegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail format")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Minumim 5, max 100")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Must match")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail format")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserTokenDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimDto> Claims { get; set; }
    }

    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
