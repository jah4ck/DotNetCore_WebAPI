function TakeToken() {
    $('input[name="SecurityToken"]').val(getSavedToken());
    true;
}

function handleError(xhr, textStatus, errorThrown) {
    if (xhr.status == 401)
        $('#responseContainer').html("Unauthorized request");
    else {
        var message = "<p>Status code: " + xhr.status + "</p>";
        message += "<pre>" + xhr.responseText + "</pre>";
        $('#responseContainer').html(message);
    }
}

function isUserLoggedIn() {
    return localStorage.getItem("token") !== null;
}

function getSavedToken() {
    return localStorage.getItem("token");
}

$.ajaxSetup({
    beforeSend: function (xhr) {
        if (isUserLoggedIn()) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + getSavedToken());
        }

    }
});

$(function () {

    $('#btLogin').click(function () {
        var token = $('input[name="__RequestVerificationToken"]').val(); //récûpération du token
        $.ajax({
            url: '@Url.Action("Token")',//appel de la method get AdresseTableau
            type: 'POST',
            data: {
                __RequestVerificationToken: token,
                username: $('#username').val(),
                password: $('#password').val()
            },
            dataType: 'html',
            success: function (e) {
                var obj = JSON.parse(e);
                var valuetoken = obj.result.Result.token;
                localStorage.setItem("token", valuetoken);
                $('#btLoginContainer').hide();
                $('#btLogoutContainer').show();
                var message = "<p>Token received and saved in local storage under the key 'token'</p>";
                message += "<p>Token Value: </p><p style='word-wrap:break-word'>" + e.Result.token + "</p>";
                $('#responseContainer').html(message);
            }
        });
    });

    $('#btLogout').click(function () {
        localStorage.removeItem("token");
        $('#btLogoutContainer').hide();
        $('#btLoginContainer').show();
        $('#responseContainer').html("<p>Token deleted from local storage</p>");
    });


    $('#btGetUserDetails').click(function () {
        $('input[name="SecurityToken"]').val('000000');
        //$.get("/home/getuserdetails").done(function(userDetails){
        // $('#responseContainer').html("<pre>" + JSON.stringify(userDetails) + "</pre>");
        //}).fail(handleError);
    });


    if (isUserLoggedIn()) {
        $('#btLoginContainer').hide();
        $('#btLogoutContainer').show();
    } else {
        $('#btLoginContainer').show();
        $('#btLogoutContainer').hide();
    }
});