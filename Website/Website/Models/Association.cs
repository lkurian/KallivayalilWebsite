using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Website.Models.ReferenceData;

namespace Website.Models
{
    public class Association : Entity
    {
        [UIHint("AssociationTypes"), DisplayName("Association Type")]
        public AssociationType Type { get; set; }

        [DisplayName("Constituent")]
        public virtual Constituent Constituent { get; set; }

        [DisplayName("ConstituentId")]
        public virtual int ConstituentId
        {
            get { return Constituent== null ? -1 : Constituent.Id; }
            set { Constituent = new Constituent {Id = value}; }
        }

        [DisplayName("AssociatedConstituentId")]
        public virtual int AssociatedConstituentId
        {
            get { return AssociatedConstituent == null ? -1 : AssociatedConstituent.Id; }
            set { AssociatedConstituent = new Constituent {Id = value}; }
        }

        [UIHint("Editor"),DisplayName("AssociatedConstituent")]
        public virtual Constituent AssociatedConstituent { get; set; } 
        
        [DisplayName("AssociatedName")]
        public virtual string AssociatedConstituentName
        { get; set; }
    }

    public class Associations : List<Association> {}
}