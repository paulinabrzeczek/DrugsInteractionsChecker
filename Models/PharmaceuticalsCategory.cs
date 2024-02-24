using System;
using System.Collections.Generic;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class PharmaceuticalsCategory
    {
        public PharmaceuticalsCategory()
        {
            Pharmaceuticals = new HashSet<Pharmaceutical>();
        }

        public int IdCategory { get; set; }
        public string NameCategory { get; set; } = null!;

        public virtual ICollection<Pharmaceutical> Pharmaceuticals { get; set; }
    }
}
