/// <reference path="../jquery-3.3.1.min.js" />
/// <reference path="../jquery.validate.min.js" />



//$(document).ready(function () {
//    $("#AddFamMemForm").validate({
//        rules: {

//            RoleId: {
//                required: true
//            },
//            FirstName: {
//                required: true
//            },
//            LastName: {
//                required: true
//            },
//            UserName: {
//                required: true
//            },
//            Password: {
//                required: true,
//                minlength: 8
//            },
//            ConfirmPassword: {
//                required: true,
//                equalTo: "#Password"
//            }

//        },
//        messages: {
//            FirstName: {
//                required: "Please enter the user's first name."
//            },
//            LastName: {
//                required: "Please enter the user's last name."
//            },
//            UserName: {
//                required: "Please enter a User Name."
//            },
//            Password: {
//                required: "Please enter a password for this user.",
//                minlength: "Password must be at least 8 characters long."
//            },
//            ConfirmPassword: {
//                required: "Re-type the password to confirm.",
//                equalTo: "Does not match Password.  Please try again."
//            }
//        },
//        errorClass: "error",
//        validClass: "valid"
//    });
//});