/// <reference path="../jquery-3.3.1.js" />
/// <reference path="../jquery.validate.js" />



$(document).ready(function ()
{
    $("#addFamForm").validate({
        rules: {
            UserRole: {
                required: true
            },
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            UserName: {
                required: true
            },
            Password: {
                required: true,

            },
            ConfirmPassword: {
                equalTo: "#Password"
            }
        },
    })
})