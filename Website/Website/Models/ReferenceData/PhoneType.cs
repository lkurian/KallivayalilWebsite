using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class PhoneType :Entity
    {
        public virtual string Description { get; set; }
    }

    public class PhoneTypes : List<PhoneType> {}
}