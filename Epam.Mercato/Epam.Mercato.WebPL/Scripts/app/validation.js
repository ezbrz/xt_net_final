function checkVal(valForCheck, type) {
    var prString;
    switch (type) {
        case 'name':
            prString = /([^ ].*)/i;
            if (valForCheck.length == 0) { message = 'Field must contain something'; return false; };
            if (valForCheck.length > 16) { message = 'Field can contain 15 chr max'; return false; };
            if (valForCheck.length < 6) { message = 'Field can contain 6 chr min'; return false; };
            if (valForCheck == '') { message = 'Field must contain something'; return false; };
            if (!(prString.test(valForCheck))) { message = 'Field can not contain only spaces'; return false; };
            break;
    }
    return true;
};

function switcher(objName) {
    var name, val;
    name = objName.attr('name');
    checkFormReady = true;
    val = objName.val();

    switch (name) {
        case 'login':
            if (checkVal(val, 'name')) {
                objName.removeClass('has-error').addClass('has-success');
                objName.closest('div').siblings('div').hide();
                objName.closest('div').siblings('div').empty();
            }
            else {
                objName.removeClass('has-success').addClass('has-error');
                objName.closest('div').siblings('div').show();
                objName.closest('div').siblings('div').empty();
                objName.closest('div').siblings('div').append(message);
                checkFormReady = false;
            }
            break;
    }
    return checkFormReady;
};
function validate() {
    $('input[name=login]').blur(function () {
        var currentObj = $(this);
        switcher(currentObj);
    });
};