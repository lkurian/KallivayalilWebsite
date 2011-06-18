using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Entity
    {
        [ScaffoldColumn(false)]
        public virtual int Id { get; set; }

        [DisplayName("CreatedDateTime")]
        public virtual DateTime? CreatedDateTime { get; set; }

        [DisplayName("UpdatedDateTime")]
        public virtual DateTime? UpdatedDateTime { get; set; }

        [DisplayName("CreatedBy")]
        public virtual string CreatedBy { get; set; }

        [DisplayName("UpdatedBy")]
        public virtual string UpdatedBy { get; set; }
    }
}