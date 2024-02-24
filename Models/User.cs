using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class User
    {
        public User()
        {
            IdReviews = new HashSet<PharmaceuticalsReview>();
        }

        public int IdUser { get; set; }
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Pole Hasło jest wymagane.")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć przynajmniej {2} znaków.", MinimumLength = 8)]
        public string Passwordhash { get; set; } = null!;

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email")]
        [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "Adres email musi zawierać znak @")]
        public string Email { get; set; } = null!;
        public int IdRole { get; set; }

        public virtual Role IdRoleNavigation { get; set; } = null!;

        public virtual ICollection<PharmaceuticalsReview> IdReviews { get; set; }
    }
}
