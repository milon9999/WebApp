// calculate currency rates
function calculate() {
    var from = document.getElementById("From");
    var to = document.getElementById("To");
    var amount = document.getElementById("Amount");

    var fVal = validate(from);
    var tVal = validate(to);
    var aVal = validate(amount);

    var showError = document.getElementById("ErrorRow");
    if (!fVal || !tVal || !aVal) {
        showError.style.display = "contents";
        var result = document.getElementById("Result");
        result.innerText = "";
    }
    else {
        showError.style.display = "none";
        var res = amount.value * to.value / from.value;
        var result = document.getElementById("Result");
        result.innerText = "Result: " + res + " " + to.selectedOptions[0].innerHTML;
    }

    
}

//validate currency rates
function validate(val) {
    var error = document.getElementById(val.id + "Error");
    if (!val.value) {
        error.style.visibility = "visible";
        val.classList.add("invalid-validate");
        return false;

    }
    else if ((val.id === 'From' || val.id === 'To') && val.value === '0') {
        //error.style.visibility = "visible";
        val.classList.add("invalid-validate");
        error.style.visibility = "hidden";
        return false;
    }
    else {
        error.style.visibility = "hidden";
        val.className = "form-control";
        return true;
    }
}


//Can type on double value
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

//Show currency rate value witch will be selected in combobox
function onchangeCombobox(evt) {
    var elem = document.getElementById(evt.target.id);

    var selectVal = document.getElementById(elem.id + "Select");
    if (elem.value) {
        if (elem.value === '0') {
            selectVal.innerText = 'NaN - cant be selected';
            selectVal.classList.add("text-danger");
        } else {
            selectVal.innerText = elem.value;
            selectVal.className = "combobox-selected";
        }
        selectVal.style.visibility = "visible";
        validate(elem);

    } else {
        selectVal.style.visibility = "hidden";
        validate(elem);
    }
    calculate();
}

    document.getElementById("Amount").addEventListener("input", updateValue);

function updateValue(e) {
    debugger;
    setTimeout(calculate(), 500);
}