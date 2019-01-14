/*function setCookie(cookieName,value,expireDays)	{
	$.cookie(cookieName, value,{ expires: expireDays});
}
function setCookie(cookieName,value)	{
	$.cookie(cookieName, value);
}
function getCookie(cookieName) {
    return $.cookie(cookieName);
}
function delCookie(cookieName) {  
    $.cookie(cookieName, null);
}
function delAllCookie(){
    $.cookie("userID", null);
    $.cookie("userName", null);
    $.cookie("fromDate", null);
    $.cookie("toDate", null);
    $.cookie("fromDate2", null);
    $.cookie("toDate2", null);
    $.cookie("currentMenu",null);
    setCookie('isSideNavOpen',null);
}*/

const ipcRenderer = require('electron').ipcRenderer;
const session = require('electron').remote.session;

function getCookie(name){           //此方法无法使用
    var cookieValue;                        
    var cookie = {
        url: "http://www.Google.com",
        name: name
    };
    session.defaultSession.cookies.get(cookie,function (error, cookies){    //原因在于这一段代码执行后根据name搜索cookie
        if(cookies.length<=0){                                              //不会等到搜索到cookie，代码就会继续走（也就是异步的）
            cookieValue = "null";                                           //这导致这个方法一定会return一个undefined
        }                                                                   //而下面这个return值的代码实际无法返回给调用者
        cookieValue = cookies[0].value;
        console.log(cookies[0].value);
        return cookieValue;         
    });
    /*session.defaultSession.cookies.get({ url: "http://www.Google.com" }, function (error, cookies) {
        console.log(cookies);
        if (cookies.length > 0) {
            let name = document.getElementById('name');
            name.value = cookies[0].value;
            let password = document.getElementById('password');
            password.value = cookies[1].value;
        }
    });*/
}

function delrCookie(name){
    session.defaultSession.cookies.remove("http://www.Google.com",name,function (error) {if (error) console.error(error);})
}

function setCookie(name, value){
  /*let Days = 30;
  let exp = new Date();
  let date = Math.round(exp.getTime() / 1000) + Days * 24 * 60 * 60;*/      //设置会话cookie
  var cookie = {
    url: "http://www.Google.com",
    name: name,
    value: value//,
    //expirationDate: date
  };
  session.defaultSession.cookies.set(cookie, (error) => {
    if (error) console.error(error);
  });
}