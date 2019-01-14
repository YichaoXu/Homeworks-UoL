function checkDate(){
    console.log($("#fromDate").val()+"-111-"+$("#toDate").val());
    var dateArray;
    var from = new Date();
    if($("#fromDate").val()!=""){
        dateArray = $("#fromDate").val().split("-");
        from.setFullYear(dateArray[0],dateArray[1]-1,dateArray[2]);
        from.setHours(0,0,0,0);
    }else{
        from="";
    }
    var to = new Date();
    if($("#toDate").val()!=""){
        dateArray = $("#toDate").val().split("-");
        to.setFullYear(dateArray[0],dateArray[1]-1,dateArray[2]);
        to.setHours(23,59,59,999);
    }else{
        to="";
    }
    $("a[id^='M']").each(function(){              //筛选部分，取<a>且id以M开头的元素
        $(this).parent().attr("id","old1");              //ready for search
        $(this).parent().hide();        //先全部隐藏
        innerID = $(this).attr("id").substring(1);
        var thisStart = new Date(eventList[innerID].startTime);
        if(from==""){
            if(to==""){ //to,from都未设置，显示全部
                $(this).parent().show();
                $(this).parent().attr("id","new1");
            }else{      //from未设置，to设置，显示开始时间时间小于to的
                if(thisStart<=to) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new1");
                }
            }
        }else {
            if(to==""){ //from设置，to未设置，显示开始时间大于from的
                if(thisStart>=from) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new1");
                }
            }else{      //from，to都设置，显示大于from，小于to的
                if(thisStart>=from&&thisStart<=to) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new1");
                }
            }
        }
    });
}
function checkDate2(){
    console.log($("#fromDate2").val()+"-222-"+$("#toDate2").val());
    var dateArray;
    var from = new Date();
    if($("#fromDate2").val()!=""){
        dateArray = $("#fromDate2").val().split("-");
        from.setFullYear(dateArray[0],dateArray[1]-1,dateArray[2]);
        from.setHours(0,0,0,0);
    }else{
        from="";
    }
    var to = new Date();
    if($("#toDate2").val()!=""){
        dateArray = $("#toDate2").val().split("-");
        to.setFullYear(dateArray[0],dateArray[1]-1,dateArray[2]);
        to.setHours(23,59,59,999);
    }else{
        to="";
    }
    console.log(eventList);
    $("a[id^='A']").each(function(){              //筛选部分，取<a>且id以A开头的元素
        $(this).parent().attr("id","old2");              //ready for search
        $(this).parent().hide();        //先全部隐藏
        innerID = $(this).attr("id").substring(1);
        var thisStart = new Date(allEventsList[innerID].startTime);
        if(from==""){
            if(to==""){ //to,from都未设置，显示全部
                $(this).parent().show();
                $(this).parent().attr("id","new2");
            }else{      //from未设置，to设置，显示开始时间时间小于to的
                if(thisStart<=to) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new2");
                }
            }
        }else{
            if(to==""){ //from设置，to未设置，显示开始时间大于from的
                if(thisStart>=from) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new2");
                }
            }else{      //from，to都设置，显示大于from，小于to的
                if(thisStart>=from&&thisStart<=to) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new2");
                }
            }
        }
    });
}
function checkDate3(){
    console.log($("#fromDate3").val()+"-333-"+$("#toDate3").val());
    var dateArray;
    var from = new Date();
    if($("#fromDate3").val()!=""){
        dateArray = $("#fromDate3").val().split("-");
        from.setFullYear(dateArray[0],dateArray[1]-1,dateArray[2]);
        from.setHours(0,0,0,0);
    }else{
        from="";
    }
    var to = new Date();
    if($("#toDate3").val()!=""){
        dateArray = $("#toDate3").val().split("-");
        to.setFullYear(dateArray[0],dateArray[1]-1,dateArray[2]);
        to.setHours(23,59,59,999);
    }else{
        to="";
    }
    console.log(popularList);
    $("a[id^='P']").each(function(){              //筛选部分，取<a>且id以A开头的元素
        $(this).parent().attr("id","old3");              //ready for search
        $(this).parent().hide();        //先全部隐藏
        innerID = $(this).attr("id").substring(1);
        var thisStart = new Date(popularList[innerID].startTime);
        if(from==""){
            if(to==""){ //to,from都未设置，显示全部
                $(this).parent().show();
                $(this).parent().attr("id","new3");
            }else{      //from未设置，to设置，显示开始时间时间小于to的
                if(thisStart<=to) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new3");
                }
            }
        }else{
            if(to==""){ //from设置，to未设置，显示开始时间大于from的
                if(thisStart>=from) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new3");
                }
            }else{      //from，to都设置，显示大于from，小于to的
                if(thisStart>=from&&thisStart<=to) {
                    $(this).parent().show();
                    $(this).parent().attr("id","new3");
                }
            }
        }
    });
}
function setMinMaxDate(number,type){
    if(number==1){
        if(type=="from"){
            $("#toDate").attr('min',$("#fromDate").val());
            setCookie("fromDate",$("#fromDate").val());
        }else if (type == "to"){
            $("#fromDate").attr('max',$("#toDate").val());
            setCookie("toDate",$("#toDate").val());
        }
        checkDate(); 
        search($("#search1"));
    }else if(number==2){
        if(type=="from"){
            $("#toDate2").attr('min',$("#fromDate2").val());
            setCookie("fromDate2",$("#fromDate2").val());
        }else if (type == "to"){
            $("#fromDate2").attr('max',$("#toDate2").val());
            setCookie("toDate2",$("#toDate2").val());
        }
        checkDate2();
        search($("#search2"));
    }else if(number==3){
        if(type=="from"){
            $("#toDate3").attr('min',$("#fromDate3").val());
            setCookie("fromDate3",$("#fromDate3").val());
        }else if (type == "to"){
            $("#fromDate3").attr('max',$("#toDate3").val());
            setCookie("toDate3",$("#toDate3").val());
        }
        checkDate3();
        search($("#search3"));
    }
       
}
function getDateFilterCookie(){
    $(document).ready(function(){
        $("#fromDate").val(getCookie("fromDate"));
        $("#toDate").val(getCookie("toDate"));
        $("#toDate").attr('min',$("#fromDate").val());
        $("#fromDate").attr('max',$("#toDate").val());
        //for date2
        $("#fromDate2").val(getCookie("fromDate2"));
        $("#toDate2").val(getCookie("toDate2"));
        $("#toDate2").attr('min',$("#fromDate2").val());
        $("#fromDate2").attr('max',$("#toDate2").val());
        //for date3
        $("#fromDate3").val(getCookie("fromDate3"));
        $("#toDate3").val(getCookie("toDate3"));
        $("#toDate3").attr('min',$("#fromDate3").val());
        $("#fromDate3").attr('max',$("#toDate3").val());
    });
}
function setCurrentDate(){
    var now = new Date();
	var yearStr = now.getFullYear();
	var monthStr = now.getMonth()+1;
	if(monthStr<10) monthStr = "0"+monthStr;
	var dayStr = now.getDate();
	if(dayStr<10) dayStr = "0"+dayStr;
	setCookie("fromDate",yearStr+"-"+monthStr+"-"+dayStr);
    setCookie("toDate","");
    setCookie("fromDate2",yearStr+"-"+monthStr+"-"+dayStr);
    setCookie("toDate2","");
    setCookie("fromDate3",yearStr+"-"+monthStr+"-"+dayStr);
    setCookie("toDate3","");
}