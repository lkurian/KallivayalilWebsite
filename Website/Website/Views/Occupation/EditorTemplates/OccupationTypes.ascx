<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.OccupationTypes>" %>

<%= Html.Telerik().DropDownList()
        .Name("OccupationType")
        .BindTo(new SelectList((IEnumerable)ViewData["occupationTypes"], "Id", "Description"))
%>
