using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Website.Models.ReferenceData;

namespace Website.Models
{
    public class Committees : List<Committee>{}

    public class Committee : Entity
    {
        [UIHint("PositionTypes"), DisplayName("Position Type")]
        public virtual PositionType Type { get; set; }

        [UIHint("StartDate"), DisplayName("Start Date")]
        public virtual DateTime StartDate { get; set; }

        [UIHint("EndDate"), DisplayName("End Date")]
        public virtual DateTime EndDate { get; set; }

        [DisplayName("Constituent")]
        public virtual Constituent Constituent { get; set; }
    }
}