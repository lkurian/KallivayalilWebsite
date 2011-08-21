using System.Runtime.Serialization;

namespace Website.Models
{
    [KnownType(typeof (ContactUs))]
    public class ContactUs : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }
        public virtual string Comments { get; set; }

    }
}