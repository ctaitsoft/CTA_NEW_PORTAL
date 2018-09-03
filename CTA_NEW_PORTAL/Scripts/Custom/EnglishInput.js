$(document).ready(function () {
    //called when key is pressed in textbox
    $(".English").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 13 && e.which != 8 && e.which != 0 &&  (e.which < 48 || e.which > 57) && (e.which < 65 || e.which > 90)  && (e.which < 97 || e.which > 122) && (e.which != 46)) {
            //display error message

           
            $(this).addClass("errorNum"); 
            $(this).css("background-color", "#FFCCCC");
            
            
            return false;
        }
        else {
            $(this).removeClass("errorNum");
            $(this).css("background-color", "#fff");
        }
    });
});




$(document).ready(function () {
    $(".English").focusout(function () {
        $(this).removeClass("errorNum");
        $(this).css("background-color", "#fff");
    });
});