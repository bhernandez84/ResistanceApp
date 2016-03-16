var useMockData = true;

var resistanceGameAPI = {
    getGames: function() {
        if (useMockData) {
            return [
                {
                    gameId: 'abcd1234',
                    dateStarted: new Date(2015, 4, 20, 0, 0, 0, 0),
                    numberOfPlayers: 5,
                    numberOfVotes: 0,
                    round: 0,
                    status: 'SetupState',
                    leader: {
                        userName: 'leader',
                        playerRole: 'resistance',
                        gameId: 'abcd1234'
                    },
                    points: {
                        spies: 0,
                        resistance: 0
                    },
                    listOfPlayers: [
                        {
                            userName: "leader",
                            playerRole: "spy",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "justin",
                            playerRole: "spy",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "ben",
                            playerRole: "resistance",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "felix",
                            playerRole: "resistance",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "penny",
                            playerRole: "resistance",
                            gameId: "abcd1234"
                        }
                    ],

                    getVote: function(playerName) {
                        return null;
                    }
                },
                {
                    gameId: 'efgh5678',
                    dateStarted: new Date(2015, 4, 20, 0, 0, 0, 0),
                    numberOfPlayers: 8,
                    numberOfVotes: 0,
                    round: 0,
                    status: 'SetupState',
                    leader: {
                        userName: 'leader',
                        playerRole: 'resistance',
                        gameId: 'efgh5678'
                    },
                    points: {
                        spies: 0,
                        resistance: 0
                    },
                    listOfPlayers: [
                        {
                            userName: "leader",
                            playerRole: "spy",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "justin",
                            playerRole: "spy",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "ben",
                            playerRole: "spy",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "felix",
                            playerRole: "resistance",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "penny",
                            playerRole: "resistance",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "jake",
                            playerRole: "resistance",
                            gameId: "abcd1234"
                        },
                        {
                            userName: "stephen",
                            playerRole: "resistance",
                            gameId: "abcd1234"
                        }, {
                            userName: "colin",
                            playerRole: "resistance",
                            gameId: "abcd1234"
                        }
                    ],

                    getVote: function(playerName) {
                        return null;
                    }
                }
            ];
        } else {
            $.ajax({
                url: "",
                data: {},
                success: function() {

                }
            });
        }

    },

    play: function() {
                
    },

    joinGame: function (gameId, playerName) {
        console.log('join game!');
        if (useMockData) {
            return {
                userName: playerName,
                playerRole: "resistance",
                gameId: gameId
            }
        } else {
            $.ajax({
                url: "",
                data: {},
                success: function () {

                }
            });
        }
    },

    getGameById: function (gameId) {
        if (useMockData) {
            var listOfGames = this.getGames();
            console.log(listOfGames);
            var myGame;
            listOfGames.filter(function(value) {
                if (value.gameId === gameId) {
                    myGame = value;
                }
            });
            return myGame;
        } else {
            $.ajax({
                url: "",
                data: {},
                success: function() {

                }
            });
        }
    },

    viewPlayers: function(gameId, sendingPlayer) {
        
    },

    vote: function () {

    },

    pickMissionMembers: function (sendingPlayer, missionMembers) {

    },

    showSpies: function (sendingPlayer) {

    },

    createGame: function(playerName) {
        console.log('create game');
    }
}