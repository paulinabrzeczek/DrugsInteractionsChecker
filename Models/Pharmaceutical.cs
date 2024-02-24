using System;
using System.Collections.Generic;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class Pharmaceutical
    {
        public Pharmaceutical()
        {
            PharmaceuticalsActiveSubstances = new HashSet<PharmaceuticalsActiveSubstance>();
            PharmaceuticalsReviews = new HashSet<PharmaceuticalsReview>();
            PharmaceuticalsSubstanceInteractions = new HashSet<PharmaceuticalsSubstanceInteraction>();
        }

        public int IdPharmaceutical { get; set; }
        public string NamePharmaceutical { get; set; } = null!;
        public string? Manufacturer { get; set; }
        public int IdCategory { get; set; }
        public string? Description { get; set; }

        public virtual PharmaceuticalsCategory IdCategoryNavigation { get; set; } = null!;
        public virtual ICollection<PharmaceuticalsActiveSubstance> PharmaceuticalsActiveSubstances { get; set; }
        public virtual ICollection<PharmaceuticalsReview> PharmaceuticalsReviews { get; set; }
        public virtual ICollection<PharmaceuticalsSubstanceInteraction> PharmaceuticalsSubstanceInteractions { get; set; }
    }
}
