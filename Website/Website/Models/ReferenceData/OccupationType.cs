using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class OccupationType :Entity
    {
        public virtual string Description { get; set; }
    }
    public class OccupationTypes : List<OccupationType> { }

}