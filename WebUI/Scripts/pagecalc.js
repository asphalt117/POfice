function chg(vel) {
    if (vel.checked) {
        var arkol = vel.parentNode.parentNode.getElementsByClassName("kol");
        if (arkol) arkol[0].className = "activekol kol";
    }
    else {
        var arkol = vel.parentNode.parentNode.getElementsByClassName("activekol kol");
        if (arkol) arkol[0].className = "kol";
    }

    calcul();
}

function calcul() {
    var result = 0;
    var arkols = document.getElementsByClassName("activekol kol");
    var resultel = document.getElementsByClassName("resultkol");

    if (arkols) {
        for (var i = 0; i < arkols.length; i++) {
            var stt = arkols[i].value.toString().replace(/\,/g, ".");
            result += parseFloat(stt);
        }
    }

    resultel[0].value = result.toFixed(3);
    resultel[1].value = result.toFixed(3);
}

function checkall(btn) {
    var chvs = document.getElementsByClassName("chv");
    var setval = false;
    var btnval = 'Отметить все записи'

    if (btn.value == btnval) {
        setval = true;
        btnval = 'Снять отметки со всех записей';
    }

    if (chvs) {
        for (var i = 0; i < chvs.length; i++) {
            if (chvs[i].checked != setval)
                chvs[i].checked = setval;

            var arkol = chvs[i].parentNode.parentNode.getElementsByClassName("kol");
            if (arkol) {
                if (setval) {
                    arkol[0].className = "activekol kol";
                }
                else {
                    arkol[0].className = "kol";
                }
            }

            calcul();
        }
    }

    btn.value = btnval;
}