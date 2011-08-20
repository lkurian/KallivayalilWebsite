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
            url: "http://localhost/Kallivayalil/Profile/Save",
            type: "POST",
            datatype: "json",
            data: person,
            accept: "application/json",
            contentType: "application/json charset=utf-8",
            success: function (data) {
                alert("HI");
            }
        });

    });
});

function getPerson() {
    var firstName = $("#FirstName").val();
    /*var gender = $("#Gender").val();
    var maritalStatus = $("#MaritialStatus").val();
    var middleName = $("#MiddleName").val();
    var branchName = $("#BranchName").val();
    var bornOn = $("#BornOn").val();
    var lastName = $("#LastName").val();
    var houseName = $("#HouseName").val();
    var diedOn = $("#DiedOn").val();*/

    return { FirstName: "a" };
/*                MiddleName: middleName,
                LastName: lastName,
                Gender: gender,
                MaritalStatus: maritalStatus,
                HouseName:houseName,
                BranchName: branchName,
                BornOn: bornOn,
                DiedOn:diedOn };*/
};