var StartDateTime = new Date();
var team1_Id = 1;
var team2_Id = 2;
var p1;


function helloClicked() {

    $.ajax({
        url: '/Home/Hello',
        dataType: 'html',
        type: 'get',
        success: function (data, textStatus, jQxhr) {

        },
        error: function (jqXhr, textStatus, errorThrown) {

        }
    })
};

$(document).ready(function () {
    bindButtons();
    incrementTime();

    var date = new Date();
    var startDateTimeString = date.getDate() + "/" + date.getMonth() + "/" + date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    $('[name=StartDateTime]').val(startDateTimeString);
});


function getScore(teamNum) {
    if (teamNum == 1) {
        return parseInt($('.team1Score')[0].value);
    }
    else return parseInt($('.team2Score')[0].value);
}

function bindButtons(){
    $('.score input').click(function () {
        document.execCommand('selectAll', false, null);
    });

    $('#resetButton').click(function () {
        var scores = $('.score input');
        console.log(scores);
        scores[0].value = 0;
        scores[1].value = 0;
    });

    $('#finishGame').submit(function () {
        $('[name=Team1_Score').val($('.team1Score').val());
        $('[name=Team2_Score').val($('.team2Score').val());

        var date = new Date();
        var endDateTimeString = date.getDate() + "/" + date.getMonth() + "/" + date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
        $('[name=EndDateTime').val(endDateTimeString);
    });

    $('.incTeam1').click(function () {
        var team1Score = getScore(1);
        if (team1Score < 30) $('.team1Score')[0].value = team1Score + 1;
    });

    $('.decTeam1').click(function () {
        var team1Score = getScore(1);
        if(team1Score > 0) $('.team1Score')[0].value = team1Score - 1;
    });

    $('.incTeam2').click(function () {
        var team2Score = getScore(2);
        if (team2Score < 30) $('.team2Score')[0].value = team2Score + 1;
    });

    $('.decTeam2').click(function () {
        var team2Score = getScore(2);
        if (team2Score > 0) $('.team2Score')[0].value = team2Score - 1;
    });

    $('.addP1__button').click(function(){
        // hide current item
        // show all add p2 buttons
        var p1Buttons = $('.addP1__button');
        var p2Buttons = $('.addP2__button');
        p1 = { id: $(this).attr('data-playerid'), image: $(this).attr('data-avatar'), name: $(this).attr('data-username') };
       // p1 = $(this).attr('data-playerid');

        p1Buttons.map(function (b) { $(p1Buttons[b]).hide(); });
        p2Buttons.map(function (b) { $(p2Buttons[b]).show(); });

    });
    $('.addP2__button').click(function () {
        // save player 2 data and call controller
        var p2 = $(this).attr('data-playerid');
       // location.href = '/home/scoreboard?p1=' + p1 + '&p2=' + p2;
        var p2 = { id: $(this).attr('data-playerid'), image: $(this).attr('data-avatar'), name: $(this).attr('data-username') };
        var requestModel = { idp1: p1.id, imagep1: p1.image, namep1: p1.name, idp2: p2.id, imagep2: p2.image, namep2: p2.name };

        $.post({
            url: "/home/ScoreBoard",
            dataType: 'html',
            type: 'post',
            contentType: 'application/json',
            data: JSON.stringify(requestModel),
            success: function (data, textStatus, jQxhr) {
                alert("success");
            },
            error: function (jqXhr, textStatus, errorThrown) {
                alert("error")
            }
        });
    });
}

function checkIfTwoPlayersSelected() {
    var visiblePlayers = $('.loginPlayer:visible');
    if (visiblePlayers.length === 2) {
        var player1 = $(visiblePlayers[0]);
        var player2 = $(visiblePlayers[1]);
        $('[name=idp1]').val(player1.attr('data-player-id'));
        $('[name=idp2]').val(player2.attr('data-player-id'));
        $('[name=imagep1]').val(player1.attr('data-player-image'));
        $('[name=imagep2]').val(player2.attr('data-player-image'));
        $('[name=namep1]').val(player1.attr('data-player-screenName'));
        $('[name=namep2]').val(player2.attr('data-player-screenName'));

        $('#playerForm').submit();
    }
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

//function finishGame() {
//    var finishGameData = {
//        "startDateTime": startDateTime,
//        "endDateTime": new Date(),
//        "Team1_score": 10,
//        "Team2_score": 10,
//        "Team1_Id": 1,
//        "Team2_Id": 2
//    }

//    //console.log(finishGameData);
//    // Post request to add game to database

//}

function beginGame() {
    var jsonPlayers;


    $.ajax({
        url: "/api/Get/Player/All",
        dataType: 'json',
        type: 'get',
        contentType: 'application/json',
        success: function (data, textStatus, jQxhr) {
            jsonPlayers = data;
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert("error");
        }
    });

    $.ajax({
        url: requestUrl,
        dataType: 'html',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(requestModel),
        success: function (data, textStatus, jQxhr) {
            toastr["success"]("Successfully changed.", featureKey);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            toastr["error"]("Feature Toggle was not changed.", featureKey);
        }
    });
}
