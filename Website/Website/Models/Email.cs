using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Website.Models.ReferenceData;

namespace Website.Models
{

    public class Emails : List<Email>
    {
    }
    public class Email : Entity
    {
        [UIHint("EmailTypes"), DisplayName("Email Type")]
        public virtual EmailType Type { get; set; }

         [DisplayName("Email Address")]
        public virtual string Address { get; set; }

        [DisplayName("Constituent")]
        public virtual Constituent Constituent { get; set; }
    }

}