function openNav() {
    if(document.getElementById("mySidenav").style.width === "15px"){
        document.getElementById("mySidenav").style.width = "400px";
        document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
        document.getElementById("openbtn").innerText = "<";
        setTimeout("document.getElementById(\"SidenavCon\").style.display = \"block\";",200);
        setCookie('isSideNavOpen','true');
    } else {
        document.getElementById("SidenavCon").style.display = "none";
        document.getElementById("mySidenav").style.width = "15px";
        document.body.style.backgroundColor = "white";
        document.getElementById("openbtn").innerText = ">";
        setCookie('isSideNavOpen','false');
    }
}

function openForm(evt, formName) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(formName).style.display = "block";
    evt.currentTarget.className += " active";

    setCookie('currentMenu',formName);
}

