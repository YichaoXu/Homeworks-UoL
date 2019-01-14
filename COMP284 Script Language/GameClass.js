/**
 * The Class which is used to create a game;
 * @param parameters: An Object which is used to initiate game parameters may be HTML
 * elements: "gameMenu, scoreBoard, gameCanvas, startScreen, gameInstruction"
 * and Object "mapSize{row, column}" which is designed to customer the map size;
 * @constructor
 */
Game = function (parameters) {
    /**
     * It will be used as default value to initiate game;
     * @type {{
     * gameMenu: HTMLElement,
     * scoreBoard: HTMLElement,
     * gameCanvas: HTMLElement,
     * startScreen: HTMLElement,
     * gameInstruction: HTMLElement,
     * mapSize: {row: number, column: number}
     * }}
     */
    var defaultComponents = {
        gameMenu: document.getElementById("gameMenu"),
        scoreBoard: document.getElementById("scoreBoard"),
        gameCanvas: document.getElementById("gameCanvas"),
        startScreen: document.getElementById("startScreen"),
        gameInstruction: document.getElementById("gameInstruction"),
        mapSize: {row: 10, column: 10}
    };

    /**
     * The private variables in this class
     */
    component = Compatibler.mergeParameters(defaultComponents, parameters);
    var scoreBoard = component.scoreBoard;
    var gameCanvas = component.gameCanvas;
    var startScreen = component.startScreen;
    var gameInstruction = component.gameInstruction;
    var mapMax = component.mapSize;
    var gameMap;
    var gameMenu = createGameMenu();

    /**
     * The only public method in the class, it will set an
     * instance of StartScreenController as the current "Game
     * Phase Controller". It means the game is in the "start"
     * phase;
     */
    this.start = function () {
        setController(new StartScreenController());
    };

    /**
     * Exit Game and set an instance of the ExitController as
     * the current "Game Phase Controller". It means the game
     * is in the "End Phase".
     * @param result The reason why exit;
     */
    function exitGame(result) {
        startScreen.innerText = result;
        startScreen.className = "visible";
        setController(new ExitController());
    }

    /**
     * Set the game controller of current phase; It will bind
     * onclick and keyboard event with respective eventHandlers
     * in GameController Class;
     * @param newController An instance of the class Phase-
     * Controller;
     */
    function setController(newController) {
        setButton(newController.buttonText, newController.buttonHandler);
        setKeyboard(newController.keyBoardInstr, newController.keyBoardHandler);
    }

    /**
     * Set the keyboard eventHandler and text in the gameInstruction
     * Area;
     * @param instr The text contains in the gameInstruction Area
     * @param handler The keyboard eventHandler;
     */
    function setKeyboard(instr, handler) {
        gameInstruction.innerText = instr;
        window.onkeypress = handler;
    }
    function setButton(title, handler) {
        var button = gameMenu.firstChild;
        button.innerText = title;
        button.onclick = handler;
    }

    /**
     * Creates a game map by the variable mapSize in the Game
     * Class. The size of the map is mapMax.row x mapMax.column
     * and the default size of the map is 10x10;
     * @returns {HTMLTableElement} A HTMLTableElement
     */
    function createMap() {
        var tmpMap = document.createElement("table");
        var r, c, tmpTr, tmpTd;
        for (r = 0; r < mapMax.row; r++) {
            tmpTr = document.createElement("tr");
            for (c = 0; c < mapMax.column; c++) {
                tmpTd = document.createElement("td");
                tmpTd.id = getIdByCoordinate(r, c);
                tmpTr.appendChild(tmpTd);
            }
            tmpMap.appendChild(tmpTr);
        }
        tmpMap.id = "map";
        return tmpMap;
    }

    /**
     * Creates an single game score label in scoreBoard which
     * can be used to store the number of the mine, robot or
     * round;
     * @param name the name of the score label;
     * @param value the value of the score label
     * @returns {HTMLSpanElement} A HTMLElement;
     */
    function createScoreLabel(name, value) {
        var tempDiv = document.createElement("td");
        var resultDiv = document.createElement("span");
        resultDiv.innerText = value;
        tempDiv.innerText = name.toUpperCase();
        tempDiv.appendChild(resultDiv);
        scoreBoard.appendChild(tempDiv);
        return resultDiv;
    }

    /**
     * Creates a game menu which is fixed in the bottom of the
     * webPage;
     * @returns {HTMLElement} A HTMLElement;
     */
    function createGameMenu() {
        var tmpMenu = component.gameMenu;
        var customButton = createMenuButton("customButton", "");
        var exitButton = createMenuButton("exitButton", "EXIT");
        exitButton.onclick = function () {exitGame("User Exit")};
        tmpMenu.appendChild(customButton);
        tmpMenu.appendChild(exitButton);
        return tmpMenu;

        function createMenuButton(id, content) {
            var tmpButton = document.createElement("div");
            tmpButton.id = id;
            tmpButton.innerText = content;
            tmpButton.className = "menuButton";
            return tmpButton;
        }

    }

    /**
     * Get the ID of a cell by the column and row of the cell
     * on the game map.
     * @param r The row of the cell in the map
     * @param c The column of the cell in the map
     * @returns {number} the id of the cell;
     */
    function getIdByCoordinate(r, c) {
        return (r < mapMax.row && r >= 0
            && c < mapMax.column && c >= 0) ?
            r * mapMax.row + c : -1;
    }

    /**
     * Get the row and column of an cell by its ID
     * @param id the id of the cell;
     * @returns {{r: number, c: number}} An object contains row
     * and column of the cell;
     */
    function getCoordinateById(id) {
        return {
            r: Math.floor(id / mapMax.row),
            c: id % mapMax.column
        };
    }

    /**
     * A "GamePhaseController" Class, it is designed to implements
     * all behavior of the game in the start phase (An addition
     * phase for customising the game map size);
     * OnclickEventHandler: Make the Start Screen dis-visible and
     * move to next phase;
     * KeyboardEventHandler: Do nothing;
     * @constructor
     */
    var StartScreenController = function () {

        this.buttonText ="START";

        this.buttonHandler = function () {
            startScreen.className = "hided";
            gameMap = createMap();
            gameCanvas.appendChild(gameMap);
            setController(new SetUpController());
        };

        this.keyBoardInstr = "Press Space To Setting Map Size!";

        this.keyBoardHandler = function () {
            var tmpRow = prompt("MAP Row","10")*1;
            if(tmpRow) mapMax.row = tmpRow;
            var tmpCol = prompt("MAP Column","10")*1;
            if(tmpCol) mapMax.column = tmpCol;
            Dialog.alertDialog("MAP SIZE", "ROW: "+tmpRow+"\nCOL: "+tmpCol )
        };
    };

    /**
     * A "GamePhaseController" Class, it is designed to implements
     * all behavior of the game in the End phase;
     * OnclickEventHandler: restart game;
     * KeyboardEventHandler: Do nothing;
     * @constructor
     */
    var ExitController = function () {

        this.buttonText ="RESTART";

        this.buttonHandler = function () {
            location.reload()
        };

        this.keyBoardInstr = "GAME OVER!";

        this.keyBoardHandler = function () {};
    };

    /**
     * A "GamePhaseController" Class, it is designed to implements
     * all behavior of the game in the SetUp phase;
     * OnclickEventHandler: Finishs and exits the "setting up" phase;
     * KeyboardEventHandler: Sets up the robot, userShip, asteroid and
     * mine;
     * @constructor initiate all variables and methods of the Class
     * and make all cell in game map be clickable for setting up;
     */
    var SetUpController = function () {
        var userShipCellID;
        var configuredCell;
        makeCellsClickable();

        this.buttonText = "FINISH";

        this.buttonHandler = exitSetUpPhase;

        this.keyBoardInstr = "| a: asteroid | m: mine | r: robot | u: user ship |";

        this.keyBoardHandler = function (ev) {
            if (!configuredCell) {
                return Dialog.alertDialog("Error", "Please, select One Cell");
            } else if (configuredCell.id === userShipCellID) {
                return Dialog.alertDialog("Error", "This cell contains user ship");
            } else if (configuredCell.className) {
                return Dialog.alertDialog("Error", "This cell has been set!");
            }
            var userType = Compatibler.getKeyCharFromEvent(ev).toLowerCase();
            switch (userType) {
                case "a":
                    setCellAs("asteroid");
                    break;
                case "m":
                    setCellAs("mine");
                    break;
                case "r":
                    setCellAs("robot");
                    break;
                case "u":
                    setCellAsUserShip();
                    break;
                default:
                    Dialog.alertDialog("Error", "Unsupported Key")
                /*Do Nothing*/
            }
        };

        /**
         * Binds the all click event of all cells with a
         * eventHandler which is used to set up the game;
         */
        function makeCellsClickable() {
            var allCells = document.getElementsByTagName("td");
            var num = allCells.length;
            while (num--) {
                allCells[num].onclick = function () {
                    configuredCell = this;
                }
            }

        }

        /**
         * Exits the setting up and move to next phase;
         */
        function exitSetUpPhase () {
            if (!userShipCellID) {
                return Dialog.alertDialog("SetUp Error", "No user ship!");
            }
            configuredCell = undefined;
            var tagStarImg = document.createElement("div");
            tagStarImg.id = "starBG";
            document.body.appendChild(tagStarImg);
            var tmpGC = new GameController();
            if (tmpGC.isGameOverAtStart()) {
                /*DoNothing*/
            }else {
                setController(tmpGC);
            }
        }

        /**
         * Sets current selected cell be "robot", "asteroid" or "mine";
         * @param types String: can be "robot", "asteroid" or "mine"
         */
        function setCellAs(types) {
            configuredCell.className = types;
        }

        /**
         * Sets current selected cell be "userShip"
         */
        function setCellAsUserShip() {
            if (userShipCellID) {
                Dialog.alertDialog("Error", "There can be at most one user ship.");
            } else {
                var tempUserShip = document.createElement("div");
                /*if IE9 or IE10 uses the Unicode to instead*/
                if(Compatibler.isIE(9)
                    || Compatibler.isIE(10)){
                    tempUserShip.innerText = "🚀";
                    console.log("IE is Best Browser!");
                }
                tempUserShip.id = "userShip";
                configuredCell.appendChild(tempUserShip);
                userShipCellID = configuredCell.id;
            }
        }
    };


    /**
     * A "GamePhaseController" Class, it is designed to implements
     * all behavior of the playing phase;
     * OnclickEventHandler: Shows the user-guide of the game;
     * KeyboardEventHandler: Makes userShip move up, down, left or
     * right and then, update all robot state;
     * @constructor initiate all variables and methods of the Class;
     */
    var GameController = function () {

        var userShip = document.getElementById("userShip");
        var roundBoard = createScoreLabel("round", 0);
        var mineBoard = createScoreLabel("mine", document.getElementsByClassName("mine").length);
        var robotBoard = createScoreLabel("robot", document.getElementsByClassName("robot").length);

        this.buttonText = "RULE";

        this.buttonHandler=function () {
            alert("RULE\n"+
                "The outcome is a win for the user if there are no robotic spaceships " +
                "left on the grid but the user's spaceship has survived;\n\n" +
                "The outcome is a win for the computer if the user's spaceship has been " +
                "destroyed and at least one robotic spaceship has survived;\n\n" +
                "Otherwise, the outcome is a draw\n"
            );
        };

        this.keyBoardInstr = "|  W: Up  |  S: Down  |  A: Right  |  d: Left  |";

        this.keyBoardHandler = function (ev) {
            var userCoord = getCoordinateById(userShip.parentElement.id);
            var userType = Compatibler.getKeyCharFromEvent(ev).toLowerCase();
            switch (userType) {
                case "w":
                    userCoord.r--;
                    userShip.className = "up";
                    break;
                case "s":
                    userCoord.r++;
                    userShip.className = "down";
                    break;
                case "a":
                    userCoord.c--;
                    userShip.className = "right";
                    break;
                case "d":
                    userCoord.c++;
                    userShip.className = "left";
                    break;
                default:
                    Dialog.alertDialog("Key", "Unsupported");
                    return;
            }
            updateOneRound(userCoord);
        };

        /**
         * Check if the game over because of the setting;
         * @returns {boolean} return true, if game over
         * immediately after setting up phase;
         */
        this.isGameOverAtStart = function(){
            return checkIfGameOver()
                || checkIfBothOfUserAndRobotCannotMove();
        };

        /**
         * Checks if both of the user and robots cannot
         * move;
         * @returns {boolean} return true, if either user
         * or robot can move;
         */
        function checkIfBothOfUserAndRobotCannotMove() {
            try {
                var useShipCell = userShip.parentElement;
                forAdjacentCellsDo(useShipCell, checkIfUserShipCanMoveToNbr);
                var allRobot = document.getElementsByClassName("robot");
                var num = allRobot.length;
                while (num--) { forAdjacentCellsDo(allRobot[num], checkIFRobotCanMoveToNbr);}
                exitGame("Draw!");
                return true;
            } catch (e) {
                console.log(e);
                return false;
            }

            function checkIfUserShipCanMoveToNbr(nbr) {
                var tempNum = Math.abs(useShipCell.id - nbr.id);
                if (tempNum !== 1 && tempNum !== mapMax.column) {return;}
                if (nbr.className !== "asteroid") {
                    throw "User CanMove to " + nbr.id;
                }/*else{
                    Cannot MOve
                }*/
            }

            function checkIFRobotCanMoveToNbr(nbr) {
                if (nbr.className !== "asteroid" && nbr.className !== "robot") {
                    throw "Robot CanMove to " + nbr.id;
                }/*else{
                    Cannot MOve
                }*/
            }

        }

        /**
         * Updates one round
         * @param coordinate the coordinate of cell where
         * the use ship try move to;
         */
        function updateOneRound(coordinate) {
            var id = getIdByCoordinate(coordinate.r, coordinate.c);
            roundBoard.innerText++;
            updateUserShip(id);
            updateAllRobots();
            checkIfGameOver();
        }

        /**
         * Update the location of the user by the cell id
         * @param cellIdthe id of cell where
         * the use ship try move to;
         */
        function updateUserShip(cellId) {
            if (cellId < 0) {
                Dialog.alertDialog("Move Cell", "Out Of Map");
                return;
            }
            var newCell = document.getElementById(cellId);
            switch (newCell.className) {
                case "mine":
                    activateMineInCell(newCell);
                    break;
                case "robot":
                    userShip.id = "destroy";
                    break;
                case "asteroid":
                    Dialog.alertDialog("Move Cell", "Occupied By asteroid");
                    return;
                /*default:
                    Do nothing*/
            }
            Compatibler.removeItSelfFromParent(userShip);
            Compatibler.appendUserShip(newCell, userShip);
        }

        function activateMineInCell(cell) {
            cell.className = "activatedMine";
            forAdjacentCellsDo(cell, function (nbr) {
                switch (nbr.className) {
                    case "robot":
                        robotBoard.innerText--;
                    case "asteroid":
                        nbr.className = "explosion";
                    /*default:
                        Do Nothing*/
                }
                nbr.isMineArea = true;
            });
            mineBoard.innerText--;
        }

        /**
         * Updates all robots location for hunting user;
         */
        function updateAllRobots() {
            var allRobotCollection = document.getElementsByClassName("robot");
            var allRobotsArr = Compatibler.htmlCollectionToArray(allRobotCollection);
            var num = allRobotsArr.length;
            while(num--){
                updateSingleRobot(allRobotsArr[num]);
            }
            setTimeout(function () {
                var allExplosion = document.getElementsByClassName("explosion");
                var num = allExplosion.length;
                while (num--) {
                    allExplosion[num].className = "";
                }
            }, 600);
        }

        /**
         * Checks whether the game is over or not
         * @returns {boolean} true, if game is over.
         */
        function checkIfGameOver() {
            if (userShip.id === "destroy") {
                exitGame("You Loss!");
            } else if (robotBoard.innerText === "0") {
                exitGame("You Win!");
            } else if (mineBoard.innerText === "0") {
                exitGame("Draw!");
            } else {
                return false;
            }
            return true;
        }

        /**
         * Updates single robot;
         * @param from
         */
        function updateSingleRobot(from) {
            try {
                var accessible = forAdjacentCellsDo(from, checkEachNeighbour);
                robotMove(huntUserShip(from, accessible));
            } catch (ignore) {
                console.log(ignore);
            }

            function checkEachNeighbour(nbr) {
                if (nbr.className === "mine") {
                    mineBoard.innerText--;
                    robotMove(nbr);
                    throw "Robot: Mine in cell "+ nbr.id
                } else if (nbr.contains(userShip)) {
                    userShip.id = "destroy";
                    robotMove(nbr);
                    throw "Robot: User in cell "+nbr.id;
                } else {
                    return (nbr.className === "asteroid"
                        || nbr.className === "robot") ? undefined : nbr;
                }
            }

            function robotMove(to) {
                if (!to) { return; }
                console.log("from: "+from.id+"\nTo: "+to.id);
                from.className = "";
                to.className = "robot";
                if (to.isMineArea) {
                    to.className = "explosion";
                    robotBoard.innerText--;
                }
            }
        }

        /**
         * Decides a best cell for hunting user ship
         * @param from where this robot is from;
         * @param surround which cells this robot can be in
         * next turn.
         * @returns {*} return null if robot cannot move.
         * else return a cell (HTMLElement);
         */
        function huntUserShip(from, surround) {
            if (!surround || surround.num === 0) {
                return;
            }
            var fromCoord = getCoordinateById(from.id);
            var userCoord = getCoordinateById(userShip.parentElement.id);
            var resultR = (fromCoord.r === userCoord.r) ? 1 : (fromCoord.r > userCoord.r) ? 0 : 2;
            var resultC = (fromCoord.c === userCoord.c) ? 1 : (fromCoord.c > userCoord.c) ? 0 : 2;
            var safetyArea = getNoMinedArea();
            return decideNextLocationFrom(safetyArea)
                || decideNextLocationFrom(surround);

            /**
             * Finds all cells which are not mined;
             * @returns {Array} a list of cell
             * Array<HTMLElement>
             */
            function getNoMinedArea() {
                var noMinedArea = [];
                noMinedArea.num = 0;
                var i, anyCell;
                for (i = 0; i < surround.length; i++) {
                    anyCell = surround[i];
                    if (anyCell && !anyCell.isMineArea) {
                        noMinedArea[i] = anyCell;
                        noMinedArea.num++;
                    }
                }
                return noMinedArea;
            }

            /**
             * Decide a cell to move from a list of cells;
             * @param area A list of cells
             * @returns {*} return null if list is empty.
             * else return a cell (HTMLElement);
             */
            function decideNextLocationFrom(area) {
                if (!area || area.length === 0) {
                    return undefined;
                }
                return area[resultR * 3 + resultC]
                    || area[3 + resultC]
                    || area[resultR * 3 + 1]
                    || randomlyDecideLocationFrom(area)
            }

            /**
             * Randomly decides a cell to move from a
             * list of cell;
             * @param area
             * @returns {*} return null if list is empty.
             * else return a cell (HTMLElement);
             */
            function randomlyDecideLocationFrom(area) {
                var radNum = Math.ceil(Math.random() * area.num);
                console.log(radNum);
                var len = area.length;
                while (len--) {
                    if (area[len]) radNum--;
                    if (!radNum) return area[len];
                }
            }
        }

        /**
         * For all adjacent cells do method "doForSingleNbr",
         * and use the eahc adjacent cell as parameter of this
         * method
         * @param cell do the method for all cells which is
         * adjacent with this cell;
         * @param doForSingleNbr the method will be done for all
         * adjacent cells;
         * @returns {*} return a list of return value of the method
         * "doForSingleNbr";
         */
        function forAdjacentCellsDo(cell, doForSingleNbr) {
            if (!cell || !doForSingleNbr) {
                return;
            }
            var cellCoord = getCoordinateById(cell.id);
            var results = [];
            results.num = 0;
            var r, c, eachNbr, nbrId, tmpResult;
            for (r = -1; r <= 1; r++) for (c = -1; c <= 1; c++) {
                if (r === 0 && c === 0) { continue; }
                nbrId = getIdByCoordinate(r + cellCoord.r, c + cellCoord.c);
                eachNbr = document.getElementById(nbrId);
                if (!eachNbr) { continue; }
                tmpResult = doForSingleNbr(eachNbr);
                if (tmpResult) {
                    results.num++;
                    results[(r + 1) * 3 + c + 1] = tmpResult;
                }
            }
            return results;
        }
    };
};
