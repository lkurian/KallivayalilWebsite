using System.Collections.Generic;

namespace Website.Models.ReferenceData
{
    public class EventType :Entity
    {
        public virtual string Description { get; set; }
    }

    public class EventTypes : List<EventType> { }
}