using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Website.Models.ReferenceData;

namespace Website.Models
{
    [KnownType(typeof (Phone))]
    public class Phone : Entity
    {
        [UIHint("PhoneTypes"),DisplayName("Phone Type")]
        public virtual PhoneType Type { get; set; }

        [DisplayName("Phone Number")]
        public virtual string Number { get; set; }

        [UIHint("Addresses"), DisplayName("Address")]
        public virtual ShortAddress Address { get; set; }

        [DisplayName("Constituent")]
        public virtual Constituent Constituent { get; set; }
    }

    public class Phones : List<Phone> {}
}