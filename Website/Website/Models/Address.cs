namespace Website.Models
{
    public class Address : Entity
    {
        public virtual string Line1 { get; set; }
        public virtual string Line2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Country { get; set; }
        public virtual int Type { get; set; }
        public virtual Constituent Constituent { get; set; }

    }
}