using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class EmailType :Entity
    {
        public virtual string Description { get; set; }
    }
    public class EmailTypes : List<EmailType> { }
}