using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class EducationType :Entity
    {
        public virtual string Description { get; set; }
    }

    public class EducationTypes : List<EducationType> { }
}