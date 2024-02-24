using System;
using System.Collections.Generic;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class ActiveSubstance
    {
        public ActiveSubstance()
        {
            ActiveSubstancesInteractionIdActiveSubstance1Navigations = new HashSet<ActiveSubstancesInteraction>();
            ActiveSubstancesInteractionIdActiveSubstance2Navigations = new HashSet<ActiveSubstancesInteraction>();
            PharmaceuticalsActiveSubstances = new HashSet<PharmaceuticalsActiveSubstance>();
        }

        public int IdActiveSubstance { get; set; }
        public string NameActiveSubstance { get; set; } = null!;

        public virtual ICollection<ActiveSubstancesInteraction> ActiveSubstancesInteractionIdActiveSubstance1Navigations { get; set; }
        public virtual ICollection<ActiveSubstancesInteraction> ActiveSubstancesInteractionIdActiveSubstance2Navigations { get; set; }
        public virtual ICollection<PharmaceuticalsActiveSubstance> PharmaceuticalsActiveSubstances { get; set; }
    }
}
