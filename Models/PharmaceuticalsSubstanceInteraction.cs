using System;
using System.Collections.Generic;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class PharmaceuticalsSubstanceInteraction
    {
        public int IdInteraction { get; set; }
        public int IdPharmaceutical { get; set; }
        public int IdSubstance { get; set; }
        public string? DescriptionInteraction { get; set; }
        public string TypeReaction { get; set; } = null!;

        public virtual Pharmaceutical IdPharmaceuticalNavigation { get; set; } = null!;
        public virtual OtherSubstance IdSubstanceNavigation { get; set; } = null!;
    }
}
