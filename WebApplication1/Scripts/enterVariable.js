userLogin = false;

function enableHref() {
    $("#insert").off("click", alertLogin);
    $("#view").off("click", alertLogin);
    $("#view").attr("href", "viewMovie.html");
    $("#insert").attr("href", "insert.html");
    $("#dataTable").off("click", alertLogin);
    $("#dataTable").attr("href", "DataTable.html");
}

function alertLogin() {
    alert("You need to login or register"); //swalalalalalalala dance!
}

function disableHref() {
    $("#view").attr("href", "");
    $("#insert").attr("href", "");
    $("#insert").click(alertLogin);
    $("#view").click(alertLogin);
    $("#dataTable").attr("href", "");
    $("#dataTable").click(alertLogin);
}

function getUserLogin() {
    if (localStorage.getItem("cgroup1_userLogin") != undefined) {
        userLogin = JSON.parse(localStorage.getItem("cgroup1_userLogin"));
    }
    else {
        userLogin = false;
        setUserLogin(false);
    }
    if (userLogin == false)
        disableHref();
    else
        logout();
}

function setUserLogin(isLogin) {
        userLogin = isLogin;
        localStorage.setItem("cgroup1_userLogin", JSON.stringify(userLogin));
}

function logout() {
    $("#login").html("Log out");
    
}

function User(name, password, id) {
    this.name = name;
    this.password = password;
    this.id = id;
}

