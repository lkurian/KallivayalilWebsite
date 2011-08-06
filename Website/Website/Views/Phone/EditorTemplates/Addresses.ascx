<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ShortAddresses>" %>

<%= Html.Telerik().DropDownList()
            .Name("ShortAddress")
            .BindTo(new SelectList((IEnumerable)ViewData["addresses"], "Id", "Description"))
%>
