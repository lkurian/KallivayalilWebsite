using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Website.Models.ReferenceData;

namespace Website.Models
{
    [KnownType(typeof (Phone))]
    public class Phone : Entity
    {
        [DisplayName("Product name")]
        public virtual PhoneType Type { get; set; }

        [DisplayName("Phone Number")]
        public virtual string Number { get; set; }

        public virtual Address Address { get; set; }
        public virtual Constituent Constituent { get; set; }
    }


    public class Phones : List<Phone> {}
}