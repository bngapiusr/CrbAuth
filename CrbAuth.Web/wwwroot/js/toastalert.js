
function Save() {
    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtlastName").val();
    var emailAddress = $("#txtEmailAddress").val();

    var objuserViewModel = { FirstName: firstName, LastName: lastName, EmailAddress: emailAddress };

    $.ajax({
        async: true,
        type: 'POST',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(objuserViewModel),
        url: '/Home/Index',
        success: function (response) {
            if (response.success === true) {
                toastr.success(response.message, 'Success Alert', { timeOut: 3000, "closeButton": true });
            } else {
                toastr.error(response.message, 'Error Alert', { timeOut: 3000, "closeButton": true });
            }
        },
        error: function () {
            toastr.error('There is some problem to process your request.', 'Error Alert', { timeOut: 3000, "closeButton": true });
        }

    });

}