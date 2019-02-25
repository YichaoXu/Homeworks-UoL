using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Risk.Model.Core{
    class GameCore {

        private static GameCore _shared;
        public static GameCore shared {
            get {
                if (_shared == null)
                    _shared = new GameCore();
                return _shared;
            }
        }

        private int _currentTurn;
        public int currentTurn {
            get {
                return _currentTurn;
            }
        }

        private GameCore() {
            _currentTurn = 1;
        }

        public void NextTurn() {
            _currentTurn += 1;
        }

        public bool MoveArmy(string from, string to) {
            return true;
        }

        public bool Attack(string from, string to, int armiesSize) {
            return true;
        }

        private void RunComputerTurn() {
            // TODO Finish AI
        }

       


    }
}
