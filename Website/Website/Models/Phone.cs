using System.Collections.Generic;
using Website.Models.ReferenceData;

namespace Website.Models
{
    public class Phone : Entity
    {

        public virtual PhoneType Type { get; set; }
        public virtual string Number { get; set; }
        public virtual Address Address { get; set; }
        public virtual Constituent Constituent { get; set; }

    }


    public class Phones : List<Phone>
    {
        
    }
}