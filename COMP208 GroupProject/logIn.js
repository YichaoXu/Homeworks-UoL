var thisUserID;
var thisUserName;
var thisAuthority;

function closeUp() {
    var name = document.getElementById("unamein").value;
    var psw = document.getElementById("pswin").value;
    //the name is in name and password is in psw, call php to verify the identity
    if (name && psw) {
        $("#loginButton").text("Connecting...");
        $.post("https://aooblog.me/COMP208/PHP/LogIn.php", {username: name, password: psw}, function (response) {
            console.log(response);
            if (response.includes("100")) {       //response返回字符串格式为000+" 0/1 "+userID or 100+" 0/1 "+userID
                alertSweet("Login", "Success");
                thisUserID = response.substring(6);
                thisUserName = name;
                thisAuthority = response.substring(4, 5);
                setCookie("userID", thisUserID);     //於：设置了cookie，刷新不会重置登录状态，
                setCookie("userName", thisUserName); //设置cookie使用的jq的方法（在cookie.js）里，没有用php的方法，如要修改，只需要修改cookie.js的方法即可
                setCookie("authority", thisAuthority);
                //於：这个延时感觉没必要啊，就把延时设为0了
                setTimeout("document.getElementById(\"login\").style.display = \"none\"", 0);
                setCurrentDate();       //把当前时间存入cookie
                getDateFilterCookie();      //设置from的值为cookie的时间（今天）
                $(".logout").html(thisUserName);
                initialiseAll();
                /*allLine();
                showAllEventsList();
                showPopularList();*/
            } else {
                if (response.includes("403 USERNAME")) {
                    $("#loginButton").text("Login");    //yu:把按钮文字改回Login
                    document.getElementsByName('unamein')[0].value = "";
                    document.getElementsByName('unamein')[0].placeholder = "this username does not exist";
                    document.getElementsByName('pswin')[0].value = "";
                }
                if (response.includes("403 PASSWORD")) {
                    $("#loginButton").text("Login");    //yu: 把按钮文字改回Login
                    document.getElementsByName('pswin')[0].value = "";
                    document.getElementsByName('pswin')[0].placeholder = "wrong password";
                }
            }
        });
    } else {
        if (name) {
            document.getElementsByName('unamein')[0].placeholder = "you did not enter username";
        }
        if (psw) {
            document.getElementsByName('pswin')[0].placeholder = "you did not enter password";
        }
        //this line only for test
        //setTimeout("document.getElementById(\"login\").style.display = \"none\"", 3000);
    }
}

function signUp() {
    var name = $("#unamereg").val();
    var psw = $("#pswreg").val();
    var email = $("#emailreg").val();
    var programme = $("#css option:selected").val();
	var counter = 0;

    if (!name || !psw || !email || !programme) {
        if (!name) $("[name=unamereg]")[0].placeholder = "you did not enter username";
        if (!psw) $("[name=pswreg]")[0].placeholder = "you did not enter password";
        if (!email) $("[name=emailreg]")[0].placeholder = "you did not enter email address";
        return;
    } 
    if(/^[0-9a-zA-Z]+$/.test(name)&&name.length > 5 &&name.length<13){
        counter++;
    }else{
        $("[name=unamereg]")[0].value = ""; 
        $("[name=unamereg]")[0].placeholder = "username: 6-12 characters";
    }
	if(/[a-zA-Z]{1,}/.test(psw)&&/^[0-9a-zA-Z]+$/.test(psw)&&psw.length > 5 &&psw.length<13){
        counter++;
    }else{
        $("[name=pswreg]")[0].value = ""; 
        $("[name=pswreg]")[0].placeholder = "password: 6-12 characters, at least 1 letter";
    }
    if(/^(?:[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])$/.test(email)){
        counter++;
    }else{
        $("[name=emailreg]")[0].value = ""; $("[name=emailreg]")[0].placeholder = "invalid email address";
    }
	
	if(counter === 3){
    $("#signupButton").text("Connecting...");
    $.post("https://aooblog.me/COMP208/PHP/SignUp.php", {
        username: name,
        password: psw,
        email: email,
        programme: programme
    }, function (response) {
        if (response.includes("100")){
            signUpSuccess();
            var tempUserID = response.split(" ")[2];
            console.log(response);
            console.log(tempUserID);
            $.post("https://aooblog.me/COMP208/PHP/AddEventsByProgramme.php", {userID:tempUserID}, function (response){
        }); 
        }
        else if (response.includes("402")){
            signUpFail(response);
        }
        else{
             warnSweet(response);
        }
    });
	}

    /*Local Method for if sign successfully*/
    function signUpSuccess() {
        signUpAlertSweet(function(){
            thisUserName = name;
            location.reload();
        });
    }

    /*Local Method for the condition if the sign is failed*/
    function signUpFail(response) {
        $("#signupButton").text("Create");
        if (response.includes("USERNAME")) {
            let nameDiv = $("[name=unamereg]")[0];
            nameDiv.value = "";
            nameDiv.placeholder = "the username has been used";
        } else if (response.includes("EMAIL")) {
            console.log("email");
            let emailDiv = $("[name=emailreg]")[0];
            emailDiv.value = "";
            emailDiv.placeholder = "the email address has been used";
        }
    }
}

function changeForm() {
    if (document.getElementById("2").style.display === "none") {
        document.getElementById("2").style.display = "block";
        document.getElementById("1").style.display = "none";
    } else {
        document.getElementById("2").style.display = "none";
        document.getElementById("1").style.display = "block";
    }
}

function initialiseAll() {
    allLine();
    showAllEventsList();
    showPopularList();
    if (getCookie('isSideNavOpen') == 'null' || getCookie('isSideNavOpen') == undefined) {
        setCookie('isSideNavOpen', 'false');
    }
    if (getCookie('currentMenu') == 'null' || getCookie('currentMenu') == undefined) {
        setCookie('currentMenu', 'myListMenu');
    }
    if (getCookie('isSideNavOpen') == 'false') {
        //do nothing
    } else if (getCookie('isSideNavOpen') == 'true') {
        openNav()
    }
    if (getCookie('currentMenu') == 'myListMenu') {
        $(".tablinks#myListButton").attr("class", "tablinks active");
        $("#myListMenu").css("display", "block");    //於：开始时根据cookie显示menu
    } else if (getCookie('currentMenu') == 'allListMenu') {
        $(".tablinks#allListButton").attr("class", "tablinks active");
        $("#allListMenu").css("display", "block");
    } else if (getCookie('currentMenu') == 'popularListMenu') {
        $(".tablinks#popularListButton").attr("class", "tablinks active");
        $("#popularListMenu").css("display", "block");
    }
    if (thisAuthority === "0") {
        $("#createdEventsButton").remove();
        $("#createNewEventButton").remove();
    }
}

function backTo() {
    if (document.getElementById("3").style.display === "none") {
        document.getElementById("3").style.display = "block";
        document.getElementById("1").style.display = "none";
    } else {
        document.getElementById("3").style.display = "none";
        document.getElementById("1").style.display = "block";
    }
}

function sendMail() {
    let email = $("#emailback").val();
    let username = $("#unameback").val();
    $.post("https://aooblog.me/COMP208/PHP/changePassword.php", {
        email: email,
        username: username
    }, function (response) {
        if (response.includes("100"))
            changeSuccess(response);
        else if(response.includes("404"))
            changeFail(response);
    });

    function changeSuccess(response) {
        if(response.includes("TRUE"))
            waitSweet("Sending","Please waiting for the email", 5000);
        else if (response.includes("FALSE"))
            warnSweet("Please try again later");

    }
    function changeFail(response) {
        if(response.includes("EMAIL"))
            warnSweet("Wrong Email Address!");
        else if (response.includes("USERNAME"))
            warnSweet("Username Don't Exist!");
        else
            warnSweet(response);
    }
}