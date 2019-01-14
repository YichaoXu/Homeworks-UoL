var a = 0;
var x = setInterval(function(){
    var hour = new Date().getHours();
    if(hour<10){hour = "0" + hour.toString()}
    var minute = new Date().getMinutes();
    if(minute<10){minute = "0" + minute.toString()}
    var second = new Date().getSeconds();
    if(second<10){second = "0" + second.toString()}
    var date = new Date().getDate();
    var month = new Date().getMonth();
    var year = new Date().getFullYear();
    if(a === 0){
        document.getElementById("clock").innerHTML = hour + " : " + minute;
    } else {
        document.getElementById("clock").innerHTML = year + "-" + month + "-" + date + "  " +  hour + " : " + minute + " : " + second;
    }
}, 100)

function showFullClock(){
    if(document.getElementById("clock").style.width==="150px"){
        document.getElementById("clock").style="width: 350px";
        setTimeout("a = 1;", 100)

    } else {
        a = 0;
        document.getElementById("clock").style="width: 150px";
    }
}

/*function viewNext(){
    loadList();
    addSite(eventList[0].locationID, eventList[0].innerID);
}*/


/*function viewPop(){
    var popList = [];
    $.post("http://localhost/PHP/getMyList.php",{userID:thisUserID, orderBy:"popularity"},data);
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
        popList.push(event);
    }

    if(popList.length < 10){var counter = popList.length;} else {var counter = 10;}
    for(var i = 0; i < counter; i++){
        addSite(popList[i].locationID, popList[i].innerID);
    }
}*/

function refresh(){
    allLine();
    showAllEventsList();
    showPopularList();
    //initMap();
    clearMarkers();
}

