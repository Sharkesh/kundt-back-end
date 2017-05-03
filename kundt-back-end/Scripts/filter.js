$("#FilterMarke").change(function (event) {
    console.log("test");
    $.ajax({
        url: "/tblAuto/FilterValidation",
        type: "POST",
        dataType: "json",
        data: { Marke: $("#FilterMarke").val() },
        success: function (data) {
            console.log("hurra");
            $("#FilterTyp").empty();
            $.each(data, function (key, value) {
                console.log("key:" + key + "value:" + value);
                $('#FilterTyp')
                     .append($('<option>', { value: value })
                     .text(value));
            });
        }
    });
});

$("#FilterMarke2").change(function (event) {
    console.log("test");
    $.ajax({
        url: "/tblAuto/FilterValidation",
        type: "POST",
        dataType: "json",
        data: { Marke: $("#FilterMarke2 :selected").text() },
        success: function (data) {
            console.log("hurra");
            $("#FilterTyp2").empty();
            $.each(data, function (key, value) {
                console.log("key:" + key + "value:" + value);
                $('#FilterTyp2')
                     .append($('<option>', { value: value })
                     .text(value));                
            });
        }
    });
});
