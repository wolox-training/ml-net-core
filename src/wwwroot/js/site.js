// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    if($('#movie-id').val())
    {
        loadData();
        $("#comment-submit").click(function () {
            newComment();
        })
    }
});

function loadData() {
    $.ajax({
        url: "/api/v1/Comment/ListComments/" + $('#movie-id').val(),
        type:"GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function(result) {
            var html = '';
            $.each(result, function (index, value) {
                html += '<div class="container">';
                html += value.text;
                html += '</div>';
                html += '<hr>';
            });
            $('.comment-component').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

/*based on mvc ajax tutorial
https://dzone.com/articles/crud-operation-in-aspnet-mvc-using-ajax-and-bootst*/

function newComment() {
    $.ajax({
        url: "/api/v1/Comment/NewComment",
        type: "POST",
        data: { 
            movieId: $('#movie-id').val(),
            viewComment: $('#new-comment').val()
        },
        dataType: "json",
        success: function (result) {
            alert("New comment added");
            loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


