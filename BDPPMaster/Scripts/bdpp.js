$(document).ready(
    $('.score').click(function () {
        document.execCommand('selectAll', false, null); 
    })

);

function helloClicked() {

    $.ajax({
        url: '/Home/Hello',
        dataType: 'html',
        type: 'get',
        success: function (data, textStatus, jQxhr) {
           
        },
        error: function (jqXhr, textStatus, errorThrown) {
           
        }
    });
}