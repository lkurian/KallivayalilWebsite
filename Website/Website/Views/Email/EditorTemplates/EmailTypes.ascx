<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.EmailTypes>" %>

<%= Html.Telerik().DropDownList()
        .Name("EmailType")
        .BindTo(new SelectList((IEnumerable)ViewData["emailTypes"], "Id", "Description"))
%>
