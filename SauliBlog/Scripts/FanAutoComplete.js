
$(document).ready(function () {

    var in_suggestion_foucos = false;

    $("#Author").focusout(function () {
        if (!in_suggestion_foucos)
        { $("#author-suggesstion-box").hide();}
    })

    $("#Author").keyup(function (){

    var serviceURL = '/Fan/AutoCompleteFan';

    $.ajax({
        type: "POST",
        url: serviceURL,
        data: { txt: document.getElementById("Author").value },
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {

    $("#author-suggesstion-box").show();
    $("#author-suggesstion-box").html(data);
    $("#Author").css("background", "#FFF");

    $(".fan-suggestion-option").click(function () {
        $("#Author").val($(this).attr("name"));
        $("#author-suggesstion-box").hide();
    });

    $("#fan-suggestion-list").mouseover(function () {
        $("#author-suggesstion-box").show();
        in_suggestion_foucos = true;
    });

    $("#fan-suggestion-list").mouseout(function () {
        $("#author-suggesstion-box").hide();
        in_suggestion_foucos = false;
    });

    

    }

    function errorFunc() {
    }
    })


})
