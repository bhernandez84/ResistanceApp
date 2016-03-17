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
                playersNeededForMission: 2,
                missionOutcomes: [
                    1,2,2,1,0
                ],
                status: 'MissionNominatingState',
                leader: {
                    userName: 'leader',
                    playerRole: 'spy',
                    gameId: 'abcd1234',
                    cardUrl: '/Assets/ally2.jpg'
                },
                points: {
                    spies: 0,
                    resistance: 0
                },
                listOfPlayers: [
                    {
                        userName: "leader",
                        playerRole: "spy",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/spy1.jpg'
                    },
                    {
                        userName: "justin",
                        playerRole: "spy",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/spy2.jpg'
                    },
                    {
                        userName: "ben",
                        playerRole: "resistance",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/ally3.jpg'
                    },
                    {
                        userName: "felix",
                        playerRole: "resistance",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/ally4.jpg'
                    },
                    {
                        userName: "penny",
                        playerRole: "resistance",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/ally5.jpg'
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

                missionOutcomes: [
                0, 0, 0, 0, 0
                ],
                status: 'SetupState',
                leader: {
                    userName: 'leader',
                    playerRole: 'resistance',
                    gameId: 'efgh5678',
                    cardUrl: '/Assets/ally2.jpg'
                },
                points: {
                    spies: 0,
                    resistance: 0
                },
                listOfPlayers: [
                    {
                        userName: "leader",
                        playerRole: "spy",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/spy1.jpg'
                    },
                    {
                        userName: "justin",
                        playerRole: "spy",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/spy2.jpg'
                    },
                    {
                        userName: "ben",
                        playerRole: "spy",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/spy3.jpg'
                    },
                    {
                        userName: "felix",
                        playerRole: "resistance",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/ally3.jpg'
                    },
                    {
                        userName: "penny",
                        playerRole: "resistance",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/ally4.jpg'
                    },
                    {
                        userName: "jake",
                        playerRole: "resistance",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/ally5.jpg'
                    },
                    {
                        userName: "stephen",
                        playerRole: "resistance",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/ally6.jpg'
                    }, {
                        userName: "colin",
                        playerRole: "resistance",
                        gameId: "abcd1234",
                        cardUrl: '/Assets/ally1.jpg'
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
                playerRole: "spy",
                gameId: gameId,
                cardUrl: '/Assets/ally1.jpg'
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