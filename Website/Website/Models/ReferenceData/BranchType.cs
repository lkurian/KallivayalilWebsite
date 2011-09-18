using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class BranchType :Entity
    {
        public virtual string Description { get; set; }
    }

    public class BranchTypes : List<BranchType> { }
}