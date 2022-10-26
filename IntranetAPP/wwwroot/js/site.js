// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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

    var adminDisp = document.getElementById("admin").style.display;
    var superAdminDisp = document.getElementById("superadmin").style.display;

    if (user.priviligeName == "Admin") {
        adminDisp = "block";
        superAdminDisp = "none";
    } else if (user.priviligeName == "SuperAdmin") {
        superAdminDisp = "block";
        adminDisp = "none";
    }
    else {
        adminDisp = "none";
        superAdminDisp = "none";
    }
}

function setLoginData()
{
    var user = JSON.parse(localStorage.getItem("user"));
    document.getElementById("username").innerHTML = user.name;
    document.getElementById("surname").innerHTML = user.surname;
}

function checkIfCorrectPrivilige(privilage, message)
{
    var user = JSON.parse(localStorage.getItem("user"));
    if (user.priviligeName != privilage) {
        window.location.href = localStorage.getItem("origin") + "/Error?Message=" + message
    }
}