using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class AssociationType :Entity
    {
        public virtual string Description { get; set; }
    }

    public class AssociationTypes : List<AssociationType> { }
}