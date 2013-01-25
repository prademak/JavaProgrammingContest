using System.ComponentModel.DataAnnotations;

namespace JavaProgrammingContest.Web.Models{
    public class RegisterExternalLogonModel{
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mailadres")]
        public string UserName { get; set; }

        [Display(Name = "Naam")]
        public string Name { get; set; }

        public string Functie { get; set; }

        public string ExternalLogonData { get; set; }
    }

    public class LocalPasswordModel{
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Huidig wachtwoord")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nieuw wachtwoord")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig nieuw wachtwoord")]
        [Compare("NewPassword", ErrorMessage = "De gegeven wachtwoorden komen niet overeen.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogonModel{
        [Required]
        [Display(Name = "e-mailadres")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Display(Name = "Herinneren")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel{
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        public string Functie { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email adres")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogon{
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}