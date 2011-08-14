
function logout() {
    $.ajax({
        url: "http://localhost/Kallivayalil/Home/Logout",
        type: "POST",
        datatype: "json",
        accept: "application/json",
        contenttype: "application/json ; charse=UTF-8"
    });

}