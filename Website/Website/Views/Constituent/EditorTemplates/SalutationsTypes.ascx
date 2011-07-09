<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.SalutationType>" %>

<%= Html.Telerik().DropDownList()
            .Name("SalutationType")
            .BindTo(new SelectList((IEnumerable)ViewData["salutationTypes"], "Id", "Description"))
%>
