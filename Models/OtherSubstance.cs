using System;
using System.Collections.Generic;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class OtherSubstance
    {
        public OtherSubstance()
        {
            PharmaceuticalsSubstanceInteractions = new HashSet<PharmaceuticalsSubstanceInteraction>();
        }

        public int IdSubstance { get; set; }
        public string NameSubstance { get; set; } = null!;

        public virtual ICollection<PharmaceuticalsSubstanceInteraction> PharmaceuticalsSubstanceInteractions { get; set; }
    }
}
