function TakeToken() {
    $('input[name="SecurityToken"]').val(getSavedToken());
    true;
}
/*
function handleError(xhr, textStatus, errorThrown) {
    if (xhr.status == 401)
        $('#responseContainer').html("Unauthorized request");
    else {
        var message = "<p>Status code: " + xhr.status + "</p>";
        message += "<pre>" + xhr.responseText + "</pre>";
        $('#responseContainer').html(message);
    }
}
*/
function isUserLoggedIn() {
    return localStorage.getItem("token") !== null;
}

function getSavedToken() {
    return localStorage.getItem("token");
}
/*
$.ajaxSetup({
    beforeSend: function (xhr) {
        if (isUserLoggedIn()) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + getSavedToken());
        }

    }
});
*/
