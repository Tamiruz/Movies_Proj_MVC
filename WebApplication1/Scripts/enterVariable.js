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
    alert("you need to log in!"); 
}

function disableHref() {
    swal({ // this will open a dialouge
        title: "You need to log in",
        text: "Press ok to log in.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    })
        .then(function (ok) {
            if (ok) location.replace("login.html");
            else {
                location.replace("login.html");
            }
        }); 
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

