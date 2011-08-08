<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.EventTypes>" %>

<%= Html.Telerik().DropDownList()
        .Name("EventType")
        .BindTo(new SelectList((IEnumerable)ViewData["eventTypes"], "Id", "Description"))
%>
