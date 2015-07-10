var startDateTime = new Date();
var team1_Id = 1;
var team2_Id = 2;

$(document).ready(function () {
    bindButtons();
    incrementTime();
});


function bindButtons(){
    $('.score').click(function () {
        document.execCommand('selectAll', false, null);
    });

    $('#resetButton').click(function () {
        var scores = $('.score input');
        console.log(scores);
        scores[0].value = 0;
        scores[1].value = 0;
    });

    $('#finishGame').click(function () {
        finishGame();
    });
}

function incrementTime(){
    var timer = $(".timer p");

    function update() {
        var myTime = timer.html();
        var ss = myTime.split(":");
        var dt = new Date();
        dt.setHours(0);
        dt.setMinutes(ss[0]);
        dt.setSeconds(ss[1]);

        var dt2 = new Date(dt.valueOf() + 1000);
        var temp = dt2.toTimeString().split(" ");
        var ts = temp[0].split(":");

        timer.html(ts[1] + ":" + ts[2]);
        setTimeout(update, 1000);
    }

    setTimeout(update, 1000);
}

function finishGame() {
    var finishGameData = {
        "startDateTime": startDateTime,
        "endDateTime": new Date(),
        "Team1_score": 10,
        "Team2_score": 10,
        "Team1_Id": 1,
        "Team2_Id": 2
    }

    //console.log(finishGameData);
    // Post request to add game to database
}