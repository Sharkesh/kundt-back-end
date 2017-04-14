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


$(document).ready(function () {
    if (document.title = "Auto Übersicht") {
        $(window).scroll(function () {

            console.log($(window).scrollTop());

            if ($(window).scrollTop() < 250) {
                $('#filter').removeClass('navbar-fixed-top');
                $('#filter').removeClass('filterPos');

            }

            if ($(window).scrollTop() > 250) {
                $('#filter').addClass('navbar-fixed-top');
                $('#filter').addClass('filterPos');
            }
        });
    }
});