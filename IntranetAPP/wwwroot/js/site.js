// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var webApiAddress = localStorage.getItem("WebApiAddress");

function checkUserData() {
    var user = JSON.parse(localStorage.getItem("user"));
    localStorage.setItem("origin", window.location.origin);

    if (user != null) {
        window.location.href = localStorage.getItem("origin") + "/main";
        document.getElementById("navigationTab").style.display = "flex";
        document.getElementById("logout").style.display = "flex";
        document.getElementById("loggedas").style.display = "inline-block";
    }
    else {
        document.getElementById("navigationTab").style.display = "none";
        document.getElementById("logout").style.display = "none";
        document.getElementById("loggedas").style.display = "none";
    }
}

function checkUserPriviliges() {
    var user = JSON.parse(localStorage.getItem("user"));

    if (user.priviligeName == "Admin") {
        document.getElementById("admin").style.display = "block";
        document.getElementById("user").style.display = "none";
        document.getElementById("superadmin").style.display = "none";
    } else if (user.priviligeName == "SuperAdmin") {
        document.getElementById("superadmin").style.display = "block";
        document.getElementById("admin").style.display = "none";
        document.getElementById("user").style.display = "none";
    } else {
        document.getElementById("admin").style.display = "none";
        document.getElementById("superadmin").style.display = "none";
        document.getElementById("user").style.display = "block";
    }
}

function setLoginData()
{
    var user = JSON.parse(localStorage.getItem("user"));
    document.getElementById("username").innerHTML = user.name;
    document.getElementById("surname").innerHTML = user.surname;
}

function checkIfCorrectPrivilige(privilage)
{
    var user = JSON.parse(localStorage.getItem("user"));
    if (user.priviligeName != privilage) {
        window.location.href = localStorage.getItem("origin") + "/Error?Message=" + "Niepoprawne uprawnienia do wykonania akcji."
    }
}

function changePassword()
{
    var user = JSON.parse(localStorage.getItem("user"));
    var oldPass = document.getElementById("changeOldPass").value;
    var newPass = document.getElementById("changeNewPass").value;
    var newPass2 = document.getElementById("changeNewPass2").value;

    var message = document.getElementById("changeMessage");
    message.innerText = "";
    message.style.color = "red";

    if (oldPass == "" || oldPass == undefined || oldPass == null) {
        message.innerText = "Pole stare hasło nie może być puste";
        return;
    }

    if (newPass == "" || newPass == undefined || newPass == null) {
        message.innerText = "Pole nowe hasło nie może być puste";
        return;
    }

    if (newPass2 == "" || newPass2 == undefined || newPass2 == null) {
        message.innerText = "Pole powtórz nowe hasło nie może być puste";
        return;
    }

    if (newPass != newPass2) {
        message.innerText = "Nowe hasła muszą być identyczne";
        return;
    }

    var body = {
        id: user.id,
        oldPassword: oldPass,
        newPassword: newPass
    };

    $.post(webApiAddress + "/ChangePassword", body, function (data) {
        if (data.statusCode == 200) {
            document.getElementById("changeOldPass").value = "";
            document.getElementById("changeNewPass").value = "";
            document.getElementById("changeNewPass2").value = "";

            message.innerText = data.message;
            message.style.color = "green";
        }
        else if (data.statusCode == 405) {
            message.innerText = data.message;
        }
        else {
            message.innerText = "Wystąpił nieoczekiwany błąd";
        }
    });
}

function addNewNews()
{
    var title = document.getElementById("newNewsTitle").value;
    var description = document.getElementById("newNewsDescription").value;

    var message = document.getElementById("newNewsMessage");
    message.innerText = "";
    message.style.color = "red";

    if (title == "" || title == undefined || title == null) {
        message.innerText = "Pole tytuł nie może być puste";
        return;
    }

    if (description == "" || description == undefined || description == null) {
        message.innerText = "Pole treść powiadomienia nie może być puste";
        return;
    }

    var body = {
        title: title,
        description: description
    };

    $.post(webApiAddress + "/AddNewNews", body, function (data) {
        if (data.statusCode == 200) {
            message.innerText = data.message;
            message.style.color = "green";
            document.getElementById("newNewsTitle").value = "";
            document.getElementById("newNewsDescription").value = "";
        }
        else {
            message.innerText = data.message;
        }
    });
}