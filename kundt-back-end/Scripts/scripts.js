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