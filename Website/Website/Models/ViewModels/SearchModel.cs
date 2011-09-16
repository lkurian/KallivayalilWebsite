using System;

namespace Website.Models.ViewModels
{
    [Serializable]
    public class SearchModel
    {
        public string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Phone { get; set; }
        public  string Email { get; set; }
        public  string OccupationName { get; set; }
        public  string OccupationDescription { get; set; }
        public  string InstituteName { get; set; }
        public  string InstituteLocation { get; set; }
        public  string Qualification { get; set; }
        public  string YearOfGraduation { get; set; }
        public  string Address { get; set; }
        public  string State { get; set; }
        public  string City { get; set; }
        public  string Country { get; set; }
        public  string Postcode { get; set; }

    }
}