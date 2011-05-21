using System;

namespace Website.Models.ViewModels
{
    [Serializable]
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}