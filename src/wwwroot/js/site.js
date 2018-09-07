// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/api/v1/CommentAPIController/GetAllComments",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(JSON.stringify(data));                  
            $("#DIV").html('');
            var DIV = '';
            $.each(data, function (i, item) {
                var rows = "<tr>" +
                    "<td id='RegdNo'>" + item.regNo + "</td>" +
                    "<td id='Name'>" + item.name + "</td>" +
                    "<td id='Address'>" + item.address + "</td>" +
                    "<td id='PhoneNo'>" + item.phoneNo + "</td>" +
                    "<td id='AdmissionDate'>" + Date(item.admissionDate,
                        "dd-MM-yyyy") + "</td>" +
                    "</tr>";
                $('#Table').append(rows);
            }); //End of foreach Loop   
            console.log(data);
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
});  