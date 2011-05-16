using System.Collections.Generic;
using Website.Models.ReferenceData;

namespace Website.Models
{

    public class Emails : List<Email>
    {
    }

    public class Email : Entity
    {
        public virtual EmailType Type { get; set; }
        public virtual string Address { get; set; }
        public virtual Constituent Constituent { get; set; }
    }

}