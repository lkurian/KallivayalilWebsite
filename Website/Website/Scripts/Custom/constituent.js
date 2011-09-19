function edit() {
    var element = $("#viewProfileDetails");
    var element1 = $("#editProfileDetails");
    if (element.hasClass("hidden")) {
        element.removeClass("hidden");
        element1.addClass("hidden");
    }
    else {
        element.addClass("hidden");
        element1.removeClass("hidden");
    }
}



$(function () {
    $("#personUpdate").click(function () {
        edit();
        var person = getPerson();

        var constituent = JSON.stringify(person);

        $.ajax({
            url: "http://localhost/Kallivayalil/Constituent/Save",
            type: "POST",
            datatype: "json",
            data: constituent,
            accept: "application/json",
            contentType: "application/json charset=utf-8",
        });

    });
});


function getPerson() {
    var firstName = $("#FirstName").val();
    var gender = $("#Gender").val();
    var maritalStatus = $("#MaritialStatus").val();
    var middleName = $("#MiddleName").val();
    var branchName = $("#BranchName").val();
    var bornOn = $("#BornOn").val();
    var lastName = $("#LastName").val();
    var houseName = $("#HouseName").val();
    var diedOn = $("#DiedOn").val();
    var isRegistered = $("#IsRegistered").val();
    var hasExpired = $("#HasExpired").val();
    var nameId = $("#NameId").val();
    var createdDateTime = $("#CreatedDateTime").val();
    var createdBy = $("#CreatedBy").val();

    return { FirstName: firstName, 
              MiddleName: middleName,
                LastName: lastName,
                Gender: gender,
                MaritalStatus: maritalStatus,
                HouseName:houseName,
                BranchName: branchName,
                BornOn: bornOn,
                DiedOn:diedOn,
                IsRegistered: isRegistered,
                HasExpired: hasExpired,
                NameId: nameId,
                CreatedDateTime: createdDateTime,
                CreatedBy:createdBy };
};