function OnLoad() {
    Active();
    StartTime();
} 

function Active() {
    if (document.title == "Übersicht") {
        $("#uebersicht").addClass("active");
    } else {
        $("#uebersicht").removeClass("active");
    }
    if (document.title == "Einstellungen") {
        $("#einstellungen").addClass("active");
    } else {
        $("#einstellungen").removeClass("active");
    }
    if (document.title == "Übersicht Autos" || document.title == "Auto Hinzufügen" || document.title == "Auto Bearbeiten") {
        $("#fahrzeug").addClass("active");
    } else {
        $("#fahrzeug").removeClass("active");
    }
    if (document.title == "Übersicht Kunden" || document.title == "Kunde Hinzufügen" || document.title == "Kunde Bearbeiten") {
        $("#kunden").addClass("active");
    } else {
        $("#kunden").removeClass("active");
    }
    if (document.title == "Übersicht Buchungen" || document.title == "Buchung Bearbeiten") {
        $("#buchung").addClass("active");
    } else {
        $("#buchung").removeClass("active");
    }
    if (document.title == "Übersicht Mitarbeiter" || document.title == "Mitarbeiter Hinzufügen" || document.title == "Mitarbeiter Bearbeiten") {
        $("#mitarbeiter").addClass("active");
    } else {
        $("#mitarbeiter").removeClass("active");
    }
}

// (SH) Uhr + Datums anzeige auf der Übersichts seite.
function StartTime() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = CheckTime(m);
    s = CheckTime(s);
    h = CheckTime(h);
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    var n = today.toLocaleDateString('de-AT', options);
    document.getElementById('time').innerHTML = n + ", " + h + ":" + m + ":" + s;
    var t = setTimeout(StartTime, 1000);

}
function CheckTime(i) {
    if (i < 10) {
        i = "0" + i
    };
    return i;
}
