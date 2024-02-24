using System;
using System.Collections.Generic;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class PharmaceuticalsActiveSubstance
    {
        public int IdPharmaceutical { get; set; }
        public int IdActiveSubstance { get; set; }
        /// <summary>
        /// mg
        /// </summary>
        public double DoseSubstance { get; set; }

        public virtual ActiveSubstance IdActiveSubstanceNavigation { get; set; } = null!;
        public virtual Pharmaceutical IdPharmaceuticalNavigation { get; set; } = null!;
    }
}
