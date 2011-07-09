<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.AddressTypes>" %>

<%= Html.Telerik().DropDownList()
        .Name("AddressType")
        .BindTo(new SelectList((IEnumerable)ViewData["addressTypes"], "Id", "Description"))
%>
