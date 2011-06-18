<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.PhoneTypes>" %>

<%= Html.Telerik().DropDownList()
        .Name("PhoneType")
        .BindTo(new SelectList((IEnumerable)ViewData["phoneTypes"], "Id", "Description"))
%>
