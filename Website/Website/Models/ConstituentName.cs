using Website.Models.ReferenceData;

namespace Website.Models
{
    public class ConstituentName :Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string PreferedName { get; set; }
        public virtual SalutationType Salutation { get; set; }

        public virtual string NameString
        {
            get { return ToString(); }
        }


        public override string ToString()
        {
            return string.Format("{3}.{0} {1} {2}", FirstName, MiddleName, LastName, Salutation.Description);
        }
    }
}