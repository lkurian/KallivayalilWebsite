
function login() {
    var userName = $("#userName").val();
    var password = $("#password").val();
    var json = { UserName: userName, Password: password};

    $.ajax({

        url: "http://localhost/Kallivayalil/Home/Login",
        type: "POST",
        data: json,
        datatype: "json",
        accept: "application/json",
        contenttype: "application/json ; charse=UTF-8",
        success: function (data) {
            alert(data);
        } 
    });
}