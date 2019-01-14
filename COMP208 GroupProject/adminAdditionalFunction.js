// Yuhao Wu 23/04

//if the user cancel to delete in the window.confirm box
function uselessFunction(){

}


function setCreationMinMaxDate(type){
	if(type=="from"){
		//make sure the end time is later than start time, if the start time is input first
		$("#endTime").attr('min',$("#startTime").val());
    }else if (type == "to"){
    	//make sure the start time is earlier than end time, if the end time is input first
		$("#startTime").attr('max',$("#endTime").val());
    }
}


function delCreatedEvent(eventID){
	$.post("https://aooblog.me/COMP208/PHP/DelCreatedEvent.php",
    	{eventID: eventID},
    		function(data){
    			console.log("id " + eventID)
    			console.log(data) //debug
    			// $("#creationResponse").html(data);
    	});
}

function popUpCreationWindow(){
	$("#creationWindow").css("display","block");
	$("#fade2").css("display","block");			    
}

function hideCreationWindow(){
	$("#creationWindow").css("display","none");
	$("#fade2").css("display","none");
}

function popUpCreatedEventsWindow(){
	$("#createdEventsWindow").css("display","block");
	$("#fade1").css("display","block");			    	
}

function hideCreatedEventsWindow(){
	$("#createdEventsWindow").css("display","none");
	$("#fade1").css("display","none");	
}

// updated by Yuhao Wu, 23/04
function createNewEvent(thisFounderName){
	var eventName 
	if (isInputValid($("#eventName").val())){ 
		$("#eventNameValidation").html("")
		eventName = $("#eventName").val();
	}	
	else
		$("#eventNameValidation").html("* Event name cannot be empty")
    
    var type
    if(document.querySelector('input[name="eventType"]:checked') != null){ // radio button VS null
    	type = document.querySelector('input[name="eventType"]:checked').value;// radio button: checked prop.
    	$("#eventTypeValidation").html("")
    }
    else
		$("#eventTypeValidation").html("* Event type cannot be empty")

    var startTime
    if($("#startTime").val() != ""){
    	startTime= $("#startTime").val();
    	$("#startTimeValidation").html("")
    }
    else
    	$("#startTimeValidation").html("* Start time should be dd/mm/yy hh:mm")

    var endTime 
    if($("#endTime").val() != ""){
    	endTime= $("#endTime").val();
    	$("#endTimeValidation").html("")
    }
    else
    	$("#endTimeValidation").html("* End time should be dd/mm/yy hh:mm")

    
  	
    var locationID;
    if($("#selectLocation").val() != null){
    	locationID = $("#selectLocation").val();
    	$("#locationValidation").html("")
    }
    else
    	$("#locationValidation").html("* location should not be empty")
    
    var brief;
    if(isInputValid($("#eventBrief").val())){
    	$("#briefValidation").html("")
    	brief = $("#eventBrief").val()
    }
    else{
		$("#briefValidation").html("* Event brief cannot be empty and word count cannot exceed 100 words");
	}
	
	if(startTime != "" && endTime != "" && (  new Date(startTime) > new Date(endTime)    )   ){
		$("#endTimeValidation").html("* startTime is later than endtime.");
		$("#endTime").val("");
		return;
	}else{
		$("#endTimeValidation").html("")
		}

    if(eventName && type && startTime && endTime && locationID && brief){
    // console.log(eventName + " " + type + " " + startTime + " " + endTime+ " "+locationID) //debug
		$.post("https://aooblog.me/COMP208/PHP/CreateNewEvent.php",
			{founderName: thisFounderName, eventName: eventName, type: type, startTime: startTime, 
				endTime: endTime, locationID: locationID,  brief: brief},
				function(data){
					alertSweet(data);
					$("#creationForm")[0].reset();  
					hideCreationWindow();
		});
		
	}
    return false;
}

//check no comma, semicolon, and blank space
function isInputValid(input){
	
	if(input.trim() == "" || input.trim().length>100){
		return false;
	}else{
		return true;
	}
}
//


function showLocationOptions(){
	$.post("https://aooblog.me/COMP208/PHP/ShowAllLocationID.php",
		function(data){
			$("#selectLocation").html(data);
		});
}

function getCreatedEventList(){
	$.post("https://aooblog.me/COMP208/PHP/GetCreatedEventList.php",
		{userID: thisUserID, userName: thisUserName, orderBy: "startTime"},
		function(data){
			$("#createdEventsWindow").html(data);
		});
}