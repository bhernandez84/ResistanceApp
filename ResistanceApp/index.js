$(window).load(function () {
    $('.loader').fadeOut('slow');
    getGames();
});



//properites
var gameModel;
var userModel;

//methods
function handleJoinSubmit(event) {
    event.preventDefault();
    var gamePicked = document.querySelector('input[name="gamePicked"]:checked') ? document.querySelector('input[name="gamePicked"]:checked').value : "";
    var userName = document.getElementById('name-input').value;
    console.log(gamePicked, userName);
    if ((gamePicked === 'newGame' || gamePicked === "") && (userName !== null || userName !== "")) {
        this.gameModel = resistanceGameAPI.createGame(userName);
    }
    else if ((userName !== null || userName !== "")) {
        this.userModel = resistanceGameAPI.joinGame(gamePicked, userName);
        this.gameModel = resistanceGameAPI.getGameById(gamePicked);
    }
    populatePlayersList();
}

function getGames() {
    var listOfGames = resistanceGameAPI.getGames();
    var $gameList = $('#game-list ul');
    listOfGames.forEach(function (item) {
        $gameList.append('<li><input type="radio" name="gamePicked" id="gamePicked" value='+item.gameId+'>' + item.gameId + '</input>')
    });
}

function populatePlayersList() {
    var listOfPlayerModels = this.gameModel.listOfPlayers;
    var isCurrentPlayerSpy = this.userModel.playerRole === 'spy';
    var $playerList = $('#players-list');
    $playerList.empty();

    if (isCurrentPlayerSpy) {
        listOfPlayerModels.forEach(function (user) {
            if (user.playerRole === 'spy') {
                $playerList.append('<li style="color: #FF0000">' + user.userName + '</li>');
            } else {
                $playerList.append('<li>' + user.userName + '</li>');
            }
        });
    } else {
        listOfPlayerModels.forEach(function(user) {
            $playerList.append('<li>' + user.userName + '</li>');
        });
    }
}

function viewPlayers() {
    $('#players-list').toggle();
}

