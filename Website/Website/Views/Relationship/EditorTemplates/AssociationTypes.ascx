<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.AddressTypes>" %>

<%= Html.Telerik().DropDownList()
        .Name("AssociationType")
            .BindTo(new SelectList((IEnumerable)ViewData["associationTypes"], "Id", "Description"))
%>
