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

        [DisplayName("Start Date"), DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date"),DataType(DataType.Date)]
        public  DateTime EndDate { get; set; }

        [UIHint("Editor"),DisplayName("Constituent")]
        public virtual Constituent Constituent { get; set; }
    }
}