<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>

<%= Html.Telerik()
    .DatePickerFor(m => m)
    .Name("StartDate")
    .OpenOnFocus(true)
    %>