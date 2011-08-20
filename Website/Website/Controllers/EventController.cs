using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
=======
using System.Web.Mvc;
>>>>>>> 50b758a0759f936fd9b3d40311dde7cda905f0e4
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.UI;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;

namespace Website.Controllers
{
    public class EventController : Controller
    {

        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
<<<<<<< HEAD
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
=======
>>>>>>> 50b758a0759f936fd9b3d40311dde7cda905f0e4
            PopulateEventTypes();
            return View();
        }


        private void PopulateEventTypes()
        {
            var eventTypesData = HttpHelper.Get<EventTypesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/EventTypes");

            var eventTypes = new EventTypes();
            mapper.MapList(eventTypesData, eventTypes, typeof (EventType));
            ViewData["eventTypes"] = eventTypes;
        }

        [GridAction]
        public ActionResult AllEvents()
        {
            return PartialView(new GridModel(GetEvents()));
        }

<<<<<<< HEAD
        private IEnumerable<Event> GetEvents()
        {
            var eventsData = HttpHelper.Get<EventsData>(string.Format("http://localhost/kallivayalilService/KallivayalilService.svc/Events?isApproved=true&startDate={0}&endDate={1}&includeBirthdays=false"
                ,DateTime.Today.AddDays(-5),DateTime.Today.AddDays(5)));
=======
        private Events GetEvents()
        {
            var eventsData = HttpHelper.Get<EventsData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Events?isApproved=true");
>>>>>>> 50b758a0759f936fd9b3d40311dde7cda905f0e4

            mapper = new AutoDataContractMapper();
            var events = new Events();
            mapper.MapList(eventsData, events, typeof (Event));
            return events;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int EventType)
        {
            var @event = new Event();
            TryUpdateModel(@event);
<<<<<<< HEAD
            var constituentId = (int)Session["constituentId"];

            @event.Constituent = new Constituent {Id = constituentId};
=======

            @event.Constituent = new Constituent {Id = 1};
>>>>>>> 50b758a0759f936fd9b3d40311dde7cda905f0e4
            @event.Type = new EventType() { Id = EventType };
            @event.ContactPerson = "test";
            @event.ContactNumber = "1232343434";
            @event.StartDate = DateTime.Today;
<<<<<<< HEAD
            @event.EndDate = DateTime.Today.AddDays(5);
=======
            @event.EndDate = DateTime.Today.AddDays(2);
>>>>>>> 50b758a0759f936fd9b3d40311dde7cda905f0e4
            @event.IsApproved = true;


            mapper = new AutoDataContractMapper();
            var eventData = new EventData();
            mapper.Map(@event, eventData);

            var newPhone = HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Events", eventData);

            return PartialView(new GridModel(GetEvents()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int EventType)
        {
            var @event = new Event();

<<<<<<< HEAD
            var constituentId = (int)Session["constituentId"];
            TryUpdateModel(@event);
            @event.Constituent = new Constituent { Id = constituentId };
=======
            TryUpdateModel(@event);
            @event.Constituent = new Constituent { Id = 1 };
>>>>>>> 50b758a0759f936fd9b3d40311dde7cda905f0e4
            @event.Type = new EventType() { Id = EventType };
            @event.ContactPerson = "test";
            @event.ContactNumber = "1232343434";
            @event.StartDate = DateTime.Today;
<<<<<<< HEAD
            @event.EndDate = DateTime.Today.AddDays(5);
=======
            @event.EndDate = DateTime.Today.AddDays(2);
>>>>>>> 50b758a0759f936fd9b3d40311dde7cda905f0e4
            @event.IsApproved = true;
            mapper = new AutoDataContractMapper();
            var eventData = new EventData();
            mapper.Map(@event, eventData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Events/{0}",id), eventData);
            return PartialView(new GridModel(GetEvents()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Events/{0}", id));
            return PartialView(new GridModel(GetEvents()));
        }
    }
}