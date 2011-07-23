using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Website.Models.ReferenceData;

namespace Website.Models
{
    [KnownType(typeof (Occupation))]
    public class Occupation : Entity
    {
        [UIHint("OccupationTypes"),DisplayName("Occupation Type")]
        public virtual OccupationType Type { get; set; }

        [DisplayName("Occupation Name")]
        public virtual string OccupationName { get; set; }

        [DisplayName("Description")]
        public virtual string Description { get; set; }

        [DisplayName("Address")]
        public virtual Address Address { get; set; }

        [DisplayName("Constituent")]
        public virtual Constituent Constituent { get; set; }
    }

    public class Occupations : List<Occupation> { }

}