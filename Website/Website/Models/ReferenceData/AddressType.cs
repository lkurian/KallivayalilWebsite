using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class AddressType :Entity
    {
        public virtual string Description { get; set; }
    }

    public class AddressTypes : List<AddressType> { }
}