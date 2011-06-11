using System;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Entity
    {
        [ScaffoldColumn(false)]
        public virtual int Id { get; set; }
        public virtual DateTime? CreatedDateTime { get; set; }
        public virtual DateTime? UpdatedDateTime { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string UpdatedBy { get; set; }

    }
}