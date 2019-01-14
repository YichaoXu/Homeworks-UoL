function setCookie(cookieName,value,expireDays)	{
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
    $.cookie("fromDate3", null);
    $.cookie("toDate3", null);
    $.cookie("currentMenu",null);
    $.cookie('isSideNavOpen',null);
    $.cookie('authority',null);
}