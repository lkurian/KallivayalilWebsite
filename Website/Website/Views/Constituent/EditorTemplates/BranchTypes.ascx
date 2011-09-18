<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.BranchTypes>" %>

<%= Html.Telerik().DropDownList()
            .Name("BranchType")
            .BindTo(new SelectList((IEnumerable)ViewData["branchTypes"], "Id", "Description"))
%>
