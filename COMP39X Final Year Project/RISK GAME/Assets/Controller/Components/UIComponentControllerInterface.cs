using UnityEngine;
using UnityEngine.UI;

namespace Risk.Controller.UIComponent {
    abstract class UIComponentControllerInterface {
        private int currentTurnNum;
        public Text gameTurnTitle { protected get; set; }
        public Text gamePhaseTitle { protected get; set; }

        protected UIComponentControllerInterface(UIComponentControllerInterface previous) {
            currentTurnNum = previous.currentTurnNum;
            gameTurnTitle = previous.gameTurnTitle;
            gamePhaseTitle = previous.gamePhaseTitle;
        }

        protected UIComponentControllerInterface() {
            currentTurnNum = 1;
        }

        public abstract void HandleSettingButtonClick();
        public abstract void HandleExitButtonClick();
        public abstract void HandleBackToGameButtonClick();
        public abstract void HandleBackToMainButton();

        public void UpdateTurnText() {
            if (gameTurnTitle == null) return;
            currentTurnNum++;
            gameTurnTitle.text = "TURN " + currentTurnNum;
        }

        public void UpdatePhaseText(string phaseStr) {
            if (gamePhaseTitle == null || phaseStr == null) return;
            gamePhaseTitle.text = phaseStr.ToUpper();
        }

        public void HandleBackToDesktopButton() {
            Application.Quit();
        }


        public void ExitGame() {
            Application.Quit();
        }
    }
}