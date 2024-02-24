using System;
using System.Collections.Generic;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class PharmaceuticalsReview
    {
        public PharmaceuticalsReview()
        {
            IdUsers = new HashSet<User>();
        }

        public int IdReview { get; set; }
        public int IdPharmaceutical { get; set; }
        public int IdUser { get; set; }
        public string Review { get; set; } = null!;
        public DateTime CreationDate { get; set; }

        public virtual Pharmaceutical IdPharmaceuticalNavigation { get; set; } = null!;

        public virtual ICollection<User> IdUsers { get; set; }
    }
}
