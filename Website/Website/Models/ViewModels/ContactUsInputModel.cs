using System;

namespace Website.Models.ViewModels
{
    [Serializable]
    public class ContactUsInputModel
    {
        public  string Name { get; set; }
        public  string Email { get; set; }
        public  string Comment { get; set; }

    }
}