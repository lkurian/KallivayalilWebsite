$(function () {
    $("#submit").click(function () {
        var feedback = { Name: $("#name").val(), Email: $("#email").val(), Comment: $("iframe").contents().find("body[spellcheck]").contents() };

        var feedbackJson = JSON.stringify(feedback);

        $.ajax({
            url: "http://localhost/Kallivayalil/ContactUs/Submit",
            type: "POST",
            datatype: "json",
            data: feedbackJson,
            accept: "application/json",
            contentType: "application/json charset=utf-8",
            success: function (data) {
                alert("HI");
            }
        });

    });
});
