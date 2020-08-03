var tecaj = 0;
var broj = 0;
var utakmica = "";
var tip = "";

var koeficijent;
var $novidiv = $("<div class='par'><div class='utakmica'></div><div class='pocetaktip'></div></div>")
$('.clickable').on('click', function () {
    console.log(model);
    if ($(this).hasClass("selected")) {
        $(this).removeClass("selected");
    }
    else
    {
        var sibilings = $(this).parent().children().not($(this)).removeClass("selected");
        koeficijent = parseInt($(this).attr("value"));
        $(this).addClass("selected");
        $(".odigraj").append($novidiv)


        tecaj += koeficijent;
    }

});



