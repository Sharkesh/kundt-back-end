function ValidateEmail(method) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!re.test($('#email' + method).val())) {
        $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeSuchen');
        $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeSuchenOhneAnimation');
        $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', true);
        $('#email' + method).css('border', 'solid 2px red');
        return false;
    } else {
        $('#email' + method).css('border', 'solid 2px green');
        return true;
    }
}

function PasswordCheck(method) {
    var password = $('#password' + method).val();
    if (password.length < 8) {
        $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeSuchen');
        $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeSuchenOhneAnimation');
        $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', true);
        $('#password' + method).css('border', 'solid 2px red')
        return false;
    } else {
        var hasUpperCase = /[A-Z]/.test(password);
        var hasLowerCase = /[a-z]/.test(password);
        var hasNumbers = /\d/.test(password);
        var hasNonalphas = /\W/.test(password);
        if (hasUpperCase + hasLowerCase + hasNumbers + hasNonalphas < 3) {
            $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeSuchen');
            $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeSuchenOhneAnimation');
            $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', true);
            $('#password' + method).css('border', 'solid 2px red')
            return false;
        } else {
            $('#password' + method).css('border', 'solid 2px green');
            return true;
        }
    }
}

function ValidateInput(method) {
    if (method == 'Login') {
        if (ValidateEmail(method) && PasswordCheck(method)) {
            $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeSuchenOhneAnimation');
            $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeSuchen');
            $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', false);
        }
    }
}