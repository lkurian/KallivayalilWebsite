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
        public Constituent Constituent { get; set; }

        [DisplayName("ConstituentId")]
        public int ConstituentId
        {
            get { return Constituent== null ? -1 : Constituent.Id; }
            set { Constituent = new Constituent {Id = value}; }
        }

        [DisplayName("AssociatedConstituentId")]
        public int AssociatedConstituentId
        {
            get { return AssociatedConstituent == null ? -1 : AssociatedConstituent.Id; }
            set { AssociatedConstituent = new Constituent {Id = value}; }
        }

        [DisplayName("AssociatedConstituent")]
        public Constituent AssociatedConstituent { get; set; }
    }

    public class Associations : List<Association> {}
}