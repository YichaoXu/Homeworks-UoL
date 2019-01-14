var eventList = [];
var allEventsList = [];
var popularList = [];
function allLine(){
    eventList=[];
    $("#showMyList").html("<a href='javascript:void(0)'>Loading...</a>");
    $.post("https://aooblog.me/COMP208/PHP/GetEventList.php",{userID:thisUserID, orderBy:"startTime", userList:true},
        function(data){
            strings = data.split("|#*#|");
            for(let i = 0; i<strings.length-1; i++){        //这里i<strings.length-1，去除了那个多出来的空元素
                thisEvent = strings[i].split("|*|");
                var event = {
                    innerID: i,
                    eventID: thisEvent[0],
                    eventName: thisEvent[1],
                    founderName: thisEvent[2],
                    startTime: thisEvent[3].substring(0,16),        //於：隐藏了秒
                    endTime: thisEvent[4].substring(0,16),
                    popularity: thisEvent[5],
                    locationID: thisEvent[6],
                    brief: thisEvent[7],
                    isAcademic: thisEvent[8]
                }
                eventList.push(event);
            }
            var text = "";
            for(let i = 0; i < eventList.length; i++){
                var thisLocationID = eventList[i].locationID;
                var thisInnerID = eventList[i].innerID;
                var thisEventName = eventList[i].eventName;
                var thisStartTime = eventList[i].startTime;
                var thisEndTime = eventList[i].endTime;
                text = text + newLine(thisInnerID, thisEventName, thisStartTime, thisEndTime, thisLocationID,"my");
            }
            //document.getElementById("showMyList").innerText = text;
            $("#showMyList").html(text);
            checkDate();
            search($("#search1"));
        });
}

/*function allLine(){                                       //於：注释的两个函数是原函数，有bug
    loadList();                                             //loadList()先被调用，loadList内进行post和php通信
    var text = "";                                          //bug在于这里的post是异步的，php的值还未返回，代码就继续往下走了
    for(var i = 0; i < eventList.length; i++){              //所以eventList会取到空值，此行还有个问题是eventList会比返回的event多一个空元素
            var thisLocationID = eventList[i].locationID;   //多一个空元素原因在于返回的字符串以；结尾，用；分割的时候会把最后的空也分出来
        var thisInnerID = eventList[i].innerID;
        var thisEventName = eventList[i].eventName;
        text = text + newLine(thisInnerID, thisEventName, thisLocationID);
    }
    console.log("adasdsad");
    document.getElementById("showMyList").innerText = text; //这里应该修改html整个元素，修改text会将<li>以字符串形式显示
}

function loadList(){
    $.post("https://aooblog.me/COMP208/PHP/getEventList.php",{userID:thisUserID, orderBy:"startTime", userList:true},
        function(data){
        console.log(data);
        strings = data.split(";");
        for(var i = 0; i<strings.length; i++){
            thisEvent = strings[i].split(",");
            var event = {
                innerID: i,
                eventID: thisEvent[0],
                eventName: thisEvent[1],
                founderName: thisEvent[2],
                startTime: thisEvent[3],
                endTime: thisEvent[4],
                popularity: thisEvent[5],
                locationID: thisEvent[6],
                brief: thisEvent[7],
                isAcademic: thisEvent[8]
            }
            eventList.push(event);
        }

    });
}*/

function thisTime(id){          //鼠标放上去时触发
    var eventListType;
    if(id.substring(0,1)=="M"){
        eventListType = eventList;
    }else if(id.substring(0,1)=="A"){
        eventListType = allEventsList;
    }else if(id.substring(0,1)=="P"){
        eventListType = popularList;
    }
    var listID=parseInt(id.substring(1));
    var timeText = 'Start: '+eventListType[listID].startTime+'<br>End: &nbsp'+eventListType[listID].endTime;
    document.getElementById(id).innerHTML = timeText;
    $("#"+id).css("font-size","large");
    //document.getElementById(id).innerText = eventList[listID].startTime + " - " + eventList[listID].endTime;
}

function godef(id){             //鼠标移开时触发
    var eventListType;
    if(id.substring(0,1)=="M"){
        eventListType = eventList;
    }else if(id.substring(0,1)=="A"){
        eventListType = allEventsList;
    }else if(id.substring(0,1)=="P"){
        eventListType = popularList;
    }
    var listID=parseInt(id.substring(1));       //於：把M去掉
    document.getElementById(id).innerText = eventListType[listID].eventName;
    $("#"+id).css("font-size","30px");
}

function showAllEventsList(){
    //$("#showAllEventsList").empty();   //每次刷新list前清空当前list(不需要了)
    allEventsList=[];
    $("#showAllEventsList").html("<a href='javascript:void(0)'>Loading...</a>");
    $.post("https://aooblog.me/COMP208/PHP/GetEventList.php",{userID:thisUserID, orderBy:"startTime", userList:false},
        function(data){
            console.log(data);
            strings = data.split("|#*#|");
            for(var i = 0; i<strings.length-1; i++){        //这里i<strings.length-1，去除了那个多出来的空元素
                thisEvent = strings[i].split("|*|");
                var event = {
                    innerID: i,
                    eventID: thisEvent[0],
                    eventName: thisEvent[1],
                    founderName: thisEvent[2],
                    startTime: thisEvent[3].substring(0,16),        //於：隐藏了秒
                    endTime: thisEvent[4].substring(0,16),
                    popularity: thisEvent[5],
                    locationID: thisEvent[6],
                    brief: thisEvent[7],
                    isAcademic: thisEvent[8]
                }
                allEventsList.push(event);
            }
            var text = "";
            for(var i = 0; i < allEventsList.length; i++){
                var thisLocationID = allEventsList[i].locationID;
                var thisInnerID = allEventsList[i].innerID;
                var thisEventName = allEventsList[i].eventName;
                var thisStartTime = allEventsList[i].startTime;
                var thisEndTime =allEventsList[i].endTime;
                text = text + newLine(thisInnerID, thisEventName, thisStartTime, thisEndTime, thisLocationID,"all");
            }
            //document.getElementById("showMyList").innerText = text;
            $("#showAllEventsList").html(text);
            checkDate2();
            search($("#search2"));
        });
}
function showPopularList(){
    //$("#showPopularList").empty();   //每次刷新list前清空当前list(不需要了)
    popularList=[];
    $("#showPopularList").html("<a href='javascript:void(0)'>Loading...</a>");
    $.post("https://aooblog.me/COMP208/PHP/GetEventList.php",{userID:thisUserID, orderBy:"popularity DESC", userList:false},
        function(data){
            strings = data.split("|#*#|");
            for(var i = 0; i<strings.length-1; i++){        //这里i<strings.length-1，去除了那个多出来的空元素
                thisEvent = strings[i].split("|*|");
                var event = {
                    innerID: i,
                    eventID: thisEvent[0],
                    eventName: thisEvent[1],
                    founderName: thisEvent[2],
                    startTime: thisEvent[3].substring(0,16),        //於：隐藏了秒
                    endTime: thisEvent[4].substring(0,16),
                    popularity: thisEvent[5],
                    locationID: thisEvent[6],
                    brief: thisEvent[7],
                    isAcademic: thisEvent[8]
                }
                console.log()
                popularList.push(event);
            }
            var text = "";
            for(var i = 0; i < popularList.length; i++){
                var thisLocationID = popularList[i].locationID;
                var thisInnerID = popularList[i].innerID;
                var thisEventName = popularList[i].eventName;
                var thisStartTime = popularList[i].startTime;
                var thisEndTime =popularList[i].endTime;
                text = text + newLine(thisInnerID, thisEventName, thisStartTime, thisEndTime, thisLocationID,"popular");
            }
            //document.getElementById("showMyList").innerText = text;
            $("#showPopularList").html(text);
            checkDate3();
            search($("#search3"));
        });
}
function newLine(thisInnerID, thisEventName, thisStartTime, thisEndTime, thisLocationID,listName){
    var hereID;
    if(listName=="my"){
        hereID = "M"+thisInnerID;
    }else if(listName=="all"){
        hereID = "A"+thisInnerID;
    }else if(listName=="popular"){
        hereID = "P"+thisInnerID;
    }
    var quest = '<li>'+
        '<a id= '+hereID+ ' href="javascript:void(0)"'+             //於:添加了href="javascript:void(0)"，鼠标移动到上面会有手指效果
        ' onclick="addSite('+thisLocationID+','+thisInnerID+',\''+thisStartTime+'\',\''+thisEndTime+'\''+',\''+hereID+'\''+')"'+   //於：修改了函数参数
        ' onmouseover="thisTime('+'\''+hereID+'\''+')"'+
        ' onmouseout="godef('+'\''+hereID+'\''+')"'+                //於：你在这里传入的是M+ID，上面函数直接调用了ID，这里参数加上了‘’
        //' ondblclick= "deleEvent(' + thisInnerID + ')">'+           //上面函数也改了，去掉了M
        '>'+
        thisEventName+'</a>'+
        /*'<br>Start: '+'<div id="startTime">'+thisStartTime+'</div>'+    //於：添加了div和id用于时间筛选
        '<br>End: '+'<div id="endTime">'+thisEndTime+'</div>'+'</a>'+*/     //注释掉的部分用于直接显示时间，不用鼠标移上去（待定）
        '</li>';
    return quest;
}
function addEventToMylist(eventID, button){
    $(button).text("Processing...");
    $(button).attr("onclick","javascript:void(0)");     //於：点击add后文字改变
    $.post("https://aooblog.me/COMP208/PHP/AddEvent.php",{eventID:eventID,userID:thisUserID},
        function(data){
            if(data.indexOf("Success!") == 0){
                alertSweet("Added", data);
                //window.location.reload();
                $(button).html("remove");
                $(button).attr("onclick","delEventFromMylist("+eventID+", this)");
                allLine();
                showAllEventsList();
                showPopularList();
            }else{
                warnSweet("Something wrong");
                $(button).html("add");
                $(button).attr("onclick","addEventToMylist("+eventID+", this)");
            }
        });
}
function delEventFromMylist(eventID, button){
    $(button).html("Processing...");
    $(button).attr("onclick","javascript:void(0)");     //於：点击remove后文字改变
    $.post("https://aooblog.me/COMP208/PHP/DelEvent.php",{eventID:eventID,userID:thisUserID},
        function(data){
            if(data.indexOf("Success!") == 0){
                alertSweet("Success", data);
                //window.location.reload();
                $(button).html("add");
                $(button).attr("onclick","addEventToMylist("+eventID+", this)");
                allLine();
                showAllEventsList();
                showPopularList();
            }else{
                warnSweet(data);
                $(button).html("remove");
                $(button).attr("onclick","delEventFromMylist("+eventID+", this)");
                allLine();
            }
        });
}