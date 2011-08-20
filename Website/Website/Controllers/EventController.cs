using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
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

            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();

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


        private IEnumerable<Event> GetEvents()
        {
            var eventsData = HttpHelper.Get<EventsData>(string.Format("http://localhost/kallivayalilService/KallivayalilService.svc/Events?isApproved=true&startDate={0}&endDate={1}&includeBirthdays=false"
                ,DateTime.Today.AddDays(-5),DateTime.Today.AddDays(5)));

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

            var constituentId = (int)Session["constituentId"];

            @event.Constituent = new Constituent {Id = constituentId};

            @event.Type = new EventType() { Id = EventType };
            @event.ContactPerson = "test";
            @event.ContactNumber = "1232343434";
            @event.StartDate = DateTime.Today;

            @event.EndDate = DateTime.Today.AddDays(5);

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


            var constituentId = (int)Session["constituentId"];
            TryUpdateModel(@event);
            @event.Constituent = new Constituent { Id = constituentId };


            @event.Type = new EventType() { Id = EventType };
            @event.ContactPerson = "test";
            @event.ContactNumber = "1232343434";
            @event.StartDate = DateTime.Today;

            @event.EndDate = DateTime.Today.AddDays(5);

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