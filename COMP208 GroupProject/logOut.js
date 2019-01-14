var justName;
function logOut() {
    logoutSweet(function (){
        delAllCookie(); //於：logout时清除所有cookie，reload后显示登录界面
        window.location.reload();
    } );
}

function showTex(){
    justName = document.getElementById("logout").innerText;
    document.getElementById("logout").innerText = "Log Out";
}

function hideTex(){
    document.getElementById("logout").innerText = justName;
}