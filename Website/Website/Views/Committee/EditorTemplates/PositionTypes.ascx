<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.PositionTypes>" %>

<%= Html.Telerik().DropDownList()
        .Name("PositionType")
            .BindTo(new SelectList((IEnumerable)ViewData["positionTypes"], "Id", "Description"))
%>
