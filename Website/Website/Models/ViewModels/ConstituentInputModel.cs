using System;

namespace Website.Models.ViewModels
{
    [Serializable]
    public class ConstituentInputModel
    {
        public  string Gender { get; set; }
        public  int BranchName { get; set; }
        public  string HouseName { get; set; }
        public  DateTime BornOn { get; set; }
        public  DateTime? DiedOn { get; set; }
        public  bool HasExpired { get; set; }
        public int MaritalStatus { get; set; }
        public  bool IsRegistered { get; set; }
          public string FirstName { get; set; }
        public  string MiddleName { get; set; }
        public  string LastName { get; set; }
        public  string PreferedName { get; set; }
        public  string CreatedBy { get; set; }
        public  DateTime CreatedDateTime { get; set; }
        public  int Salutation { get; set; }
        public  int NameId { get; set; }

    }
}