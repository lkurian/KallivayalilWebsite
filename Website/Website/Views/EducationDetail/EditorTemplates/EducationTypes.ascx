﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Website.Models.ReferenceData.EducationTypes>" %>

<%= Html.Telerik().DropDownList()
            .Name("EducationType")
            .BindTo(new SelectList((IEnumerable)ViewData["educationTypes"], "Id", "Description"))
%>
