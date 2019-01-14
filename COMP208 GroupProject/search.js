function search(thisItem){
    console.log($(thisItem).val());
    console.log($(thisItem).attr("id"));
    /*if($(thisItem).val()==""){
        console.log("empty");
        return;
    }*/
    if($(thisItem).attr("id") == "search1"){
        searchFilter(1, $(thisItem).val());
    }else if($(thisItem).attr("id") == "search2"){
        searchFilter(2, $(thisItem).val());
    }else if($(thisItem).attr("id") == "search3"){
        searchFilter(3, $(thisItem).val());
    }
}

function searchFilter(searchID, context){    //对第1,2,3列表进行搜索筛选
    idFilter = "li#new"+searchID;
    if(context == ""){
        $(idFilter).each(function(){
            $(this).show();
            return;
        });
    }
    $(idFilter).each(function(){
        $(this).show();
        if($(this).children("a").text().toLowerCase().indexOf(context.toLowerCase()) == -1){
            $(this).hide();
        }
    });
}


/*var searchRes = [];

function search(str) {
    if (str.length==0) {
        document.getElementById("liveresult").innerHTML="";
        document.getElementById("liveresult").style.border="0px";
        return;
    }
    if (window.XMLHttpRequest) {
        // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp=new XMLHttpRequest();
    } else {  // code for IE6, IE5
        xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange=function() {
        if (this.readyState==4 && this.status==200) {
            var text = "";
            var strings=this.responseText.split(";");
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
                searchRes.push(event);
            }
            for(var i = 0; i < searchRes.length; i++){
                var thisLocationID = searchRes[i].locationID;
                var thisInnerID = searchRes[i].innerID;
                var thisEventName = searchRes[i].eventName;
                text = text + newSLine(thisInnerID, thisEventName, thisLocationID);
            }
            document.getElementById("resultlineup").innerText = text;
        }
    }
    xmlhttp.open("POST","livesearch.php",true);
    xmlhttp.send(str);
}

function newSLine(thisInnerID, thisEventName, thisLocationID){
    var hereID = "S"+thisInnerID;
    var quest = "<li><a id= "+hereID+" onclick=\"addSite("+thisLocationID+","+thisInnerID+")\" onmouseover=\"thisSTime("+hereID+")\" onmouseout=\"goSdef("+hereID+")\" ondblclick = \"addEvent(" + thisInnerID + ")\">"+thisEventName+"</a></li>";
    return quest;
}

function thisSTime(id){
    document.getElementById(id).innerText = eventList[id].startTime + " - " + eventList[id].endTime;
}

function goSdef(id){
    document.getElementById(id).innerText = eventList[id].eventName;
}

function addEvent(id) {
    $.post("https://aooblog.me/COMP208/PHP/AddEvent.php",{userID:thisUserID, eventID:searchRes[id].eventID},data);
}*/