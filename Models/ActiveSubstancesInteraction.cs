using System;
using System.Collections.Generic;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class ActiveSubstancesInteraction
    {
        public int IdInteractionSubstance { get; set; }
        public int IdActiveSubstance1 { get; set; }
        public int IdActiveSubstance2 { get; set; }
        public string? DescriptionInteraction { get; set; }
        public string TypeReaction { get; set; } = null!;

        public virtual ActiveSubstance IdActiveSubstance1Navigation { get; set; } = null!;
        public virtual ActiveSubstance IdActiveSubstance2Navigation { get; set; } = null!;
    }
}
