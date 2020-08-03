
var rezultati = $.connection.rezultatiHub;
var gol = 0;
var id;
console.log("admin");
$.connection.hub.start({ transport: ['webSockets', 'serverSentEvents', 'longPolling'] })
    .done(function () {
                        
        });


   
function posaljigoldomacin(id) {
    
    rezultati.server.updateScore(id, gol, "domacin");
}
function posaljigolgost(id)
{
   
  
    rezultati.server.updateScore(id, gol, "gost");
}
