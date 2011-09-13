using System;
using System.Collections.Generic;

namespace Website.Models
{
    public class SearchResults : List<SearchResult> { }
    public class SearchResult : Entity
    {
        public virtual string Gender { get; set; }
        public virtual int BranchName { get; set; }
        public virtual string HouseName { get; set; }
        public virtual DateTime BornOn { get; set; }
        public virtual DateTime? DiedOn { get; set; }
        public virtual bool HasExpired { get; set; }
        public virtual int MaritialStatus { get; set; }
        public virtual bool IsRegistered { get; set; }
        public virtual ConstituentName Name { get; set; }
        public virtual Phones Phones { get; set; }
        public virtual Emails Emails { get; set; }
        public virtual Addresses Addresses { get; set; }
        public virtual Occupations Occupations { get; set; }
        public virtual EducationDetails EducationDetails { get; set; }


    }
}