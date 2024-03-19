var submitbutton = document.getElementById('contact-submit');

submitbutton.onclick = function () {

    if (ValidateElememts() == false) {
        return false;
    }

    var formData = {
        FullName: document.getElementById('fullname').value,
        Email: document.getElementById('email').value,
        Subject: document.getElementById('subject').value,
        Message: document.getElementById('message').value,
        GoogleRecaptchaToken: document.getElementById('googleRecaptchaToken').value
    };

    var submitSuccessMessage = document.getElementById('submitSuccessMessage');
    var submitErrorMessage = document.getElementById('submitErrorMessage');

    fetch("/umbraco/api/contactus/insert", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formData)
    }).then(res => {
        if (!res.ok) {
            console.log(res.blob());
            throw new Error('Something went wrong, ' + res);

        } else if (res.ok) {
            console.log("Request complete!");

            submitSuccessMessage.classList.remove("d-none");
            submitErrorMessage.classList.add("d-none");
        } else {
            console.error("My Errors else" + res.status);
            submitErrorMessage.classList.remove("d-none");
            submitSuccessMessage.classList.add("d-none");
        }
    }).catch((err) => {
        console.error("My Errors" + err);
        submitErrorMessage.classList.remove("d-none");
        submitSuccessMessage.classList.add("d-none");
    });
}

function ValidateElememts() {
    //if (document.getElementById('fullname').value.trim() == "") {
    //    document.getElementById('fullnameValidation').classList.remove("d-none");
    //    return false;
    //} else {
    //    document.getElementById('fullnameValidation').classList.add("d-none");
    //}

    if (document.getElementById('email').value.trim() == "") {
        document.getElementById('emailValidation').classList.remove("d-none");
        return false;
    } else {
        document.getElementById('emailValidation').classList.add("d-none");
    }
    if (document.getElementById('subject').value.trim() == "") {
        document.getElementById('subjectValidation').classList.remove("d-none");
        return false;
    } else {
        document.getElementById('subjectValidation').classList.add("d-none");
    }

    if (document.getElementById('message').value.trim() == "") {
        document.getElementById('messageValidation').classList.remove("d-none");
        return false;
    } else {
        document.getElementById('messageValidation').classList.add("d-none");
    }

    if (document.getElementById('googleRecaptchaToken').value.trim() == "") {
        document.getElementById('googleRecaptchaTokenValidation').classList.remove("d-none");
        return false;
    } else {
        document.getElementById('googleRecaptchaTokenValidation').classList.add("d-none");
        return true;
    }
}


var onloadCallback = function () {
    grecaptcha.render('html_element', {
        'sitekey': sitekey,
        'callback': verifyCallback
    });
};

var verifyCallback = function (response) {
    document.getElementById("googleRecaptchaToken").value = response;
};