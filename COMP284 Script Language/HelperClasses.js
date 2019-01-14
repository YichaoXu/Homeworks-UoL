/**
 * The classes is used to make the program can
 * be executed in any kind of browser core;
 * @constructor
 */
Compatibler = function () {
};

Compatibler.addEventListener = function (element, eventType, handler) {
    if (element.addEventListener) {
        element.addEventListener(eventType, handler);
    } else {
        element.attachEvent("on" + eventType, handler);
    }
};

Compatibler.removeItSelfFromParent = function (element) {
    if (element.remove) {
        element.remove()
    } else {
        element.parentNode.removeChild(element);
    }
};

Compatibler.appendUserShip = function(cell, userShip){
    cell.appendChild(userShip);
    if(Compatibler.isIE(9)
        || Compatibler.isIE(10)){
        userShip.innerText = "🚀";
        console.log("IE");
    }
};

Compatibler.mergeParameters = function (objA, objB) {
    /*If Object.assign Can be used*/
    if (Object.assign) {
        return Object.assign(objA, objB);
    }
    /*Else use naive assign mechanism*/

    var result = clone(objA);
    if(objB) for (var eachKey in objB) {
        result[eachKey] = objB[eachKey]
    }
    return result;

    /*Naive copy*/
    function clone(obj) {
        if (null == obj || "object" !== typeof obj) return obj;
        var copy = obj.constructor();
        for (var attr in obj) {
            if (obj.hasOwnProperty(attr)) copy[attr] = obj[attr];
        }
        return copy;
    }
};

Compatibler.getKeyCharFromEvent = function (ev) {
    ev = ev || window.event;
    var charCode = ev.keyCode || ev.which || ev.charCode;
    return String.fromCharCode(charCode);
};

Compatibler.htmlCollectionToArray = function(hTMLCollection){
    return(Array.from) ? Array.from(hTMLCollection) : [].slice.call(hTMLCollection);
};

Compatibler.isIE = function(ver){
    if(ver>9){return "onpropertychange" in document && !!window.matchMedia}
    var b = document.createElement('b')
    b.innerHTML = '<!--[if IE ' + ver + ']><i></i><![endif]-->';
    return b.getElementsByTagName('i').length === 1;
}

/**
 * The classes is used to generate a dialog which
 * is used to instead of the "alert" in JavaScript
 * @constructor
 */
Dialog = function() {};



Dialog.alertDialog = function(title, contains, behaviors){
    document.getElementById("dialogTitle").innerText = title;
    document.getElementById("dialogText_alert").innerText = contains;
    document.getElementById("dialogButton").onclick = behaviors||closeDialog;
    var dialogScreen = document.getElementById("dialogScreen");
    dialogScreen.className =  "alertBox centre";

    function closeDialog() {
        dialogScreen.className =  "alertBox centre hided";
    }
};
