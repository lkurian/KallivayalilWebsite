using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class SalutationType :Entity
    {
        public virtual string Description { get; set; }
    }

    public class SalutationTypes : List<SalutationType> { }
}