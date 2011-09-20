using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class PositionType : Entity
    {
        public virtual string Description { get; set; }
    }

    public class PositionTypes : List<PositionType> { }
}