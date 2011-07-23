using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Website.Models.ReferenceData;

namespace Website.Models
{
    public class Address : Entity
    {
        [DisplayName("Line1")]
        public virtual string Line1 { get; set; }
        
        [DisplayName("Line2")]
        public virtual string Line2 { get; set; }
        
        [DisplayName("City")]
        public virtual string City { get; set; }
        
        [DisplayName("State")]
        public virtual string State { get; set; }
        
        [DisplayName("Postcode")]
        public virtual string PostCode { get; set; }
        
        [DisplayName("Country")]
        public virtual string Country { get; set; }

        [UIHint("AddressTypes"), DisplayName("Address Type")]
        public virtual AddressType Type { get; set; }

        [DisplayName("Constituent")]
        public virtual Constituent Constituent { get; set; }

    }

    public class Addresses : List<Address> { }
}