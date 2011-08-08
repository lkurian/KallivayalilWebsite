using System.Collections.Generic;
using System.ComponentModel;

namespace Website.Models
{
    public class ShortAddress : Entity
    {
        [DisplayName("Description")]
        public virtual string Description { get; set; }
             
    }

    public class ShortAddresses : List<ShortAddress> { }
}