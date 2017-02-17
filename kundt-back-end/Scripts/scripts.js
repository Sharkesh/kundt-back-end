function Active() {
    if (document.title == "Übersicht") {
        $("#uebersicht").addClass("active");
    } else {
        $("#uebersicht").removeClass("active");
    }
    if (document.title == "Übersicht Autos" || document.title == "Auto Hinzufügen") {
        $("#fahrzeug").addClass("active");
    } else {
        $("#fahrzeug").removeClass("active");
    }
    if (document.title == "Übersicht Kunden" || document.title == "Kunde Hinzufügen") {
        $("#kunden").addClass("active");
    } else {
        $("#kunden").removeClass("active");
    }
    if (document.title == "Übersicht Buchungen") {
        $("#buchung").addClass("active");
    } else {
        $("#buchung").removeClass("active");
    }
    if (document.title == "Übersicht Mitarbeiter" || document.title == "Mitarbeiter Hinzufügen") {
        $("#mitarbeiter").addClass("active");
    } else {
        $("#mitarbeiter").removeClass("active");
    }

    startTime();
}
// (SH) Uhr + Datums anzeige auf der Übersichts seite.
function startTime() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    var d = new Date();
    var n = d.toDateString();
    document.getElementById('time').innerHTML =
    n+", "+h + ":" + m + ":" + s;
    var t = setTimeout(startTime, 500);

}
function checkTime(i) {
    if (i < 10) { i = "0" + i };  
    return i;
}