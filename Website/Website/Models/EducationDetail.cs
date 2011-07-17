using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Website.Models.ReferenceData;

namespace Website.Models
{
    [KnownType(typeof (EducationDetail))]
    public class EducationDetail : Entity
    {
        [UIHint("EducationTypes"),DisplayName("Education Type")]
        public virtual EducationType Type { get; set; }

        [DisplayName("Qualification")]
        public virtual string Qualification { get; set; }
        
        [DisplayName("InstituteName")]
        public virtual string InstituteName { get; set; } 
        
        [DisplayName("InstituteLocation")]
        public virtual string InstituteLocation { get; set; }    
        
        [DisplayName("YearOfGraduation")]
        public virtual string YearOfGraduation { get; set; }

        [DisplayName("Constituent")]
        public virtual Constituent Constituent { get; set; }
    }

    public class EducationDetails : List<EducationDetail> { }
}