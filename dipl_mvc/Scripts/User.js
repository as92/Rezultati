

var rezultati = $.connection.rezultatiHub;
//var utakmice ='@Html.Raw(Json.Encode(@ViewBag.utak))';
//console.log(utakmice);

$.connection.hub.start({ transport: ['webSockets', 'serverSentEvents', 'longPolling'] })
    .done(function () {


    });



rezultati.client.getScore = function (id, gol, strana) {
    
    
}