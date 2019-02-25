using UnityEngine;
using UnityEngine.UI;

namespace Risk.Controller.UIComponent {

    class RiskUIComponentController : UIComponentControllerInterface {
        public GameObject gameResultPanel { protected get; set; }
        public GameObject gameSettingPanel { protected get; set; }

        public RiskUIComponentController(RiskUIComponentController previous) : base(previous) {
            gameResultPanel = previous.gameResultPanel;
            gameSettingPanel = previous.gameSettingPanel;
        }

        public RiskUIComponentController() : base() {
            if (gameResultPanel != null) gameResultPanel.SetActive(false);
            if (gameSettingPanel != null) gameSettingPanel.SetActive(false);
        }


        public override void HandleSettingButtonClick() {
            gameSettingPanel.SetActive(true);
        }

        public override void HandleExitButtonClick() {
            gameResultPanel.SetActive(true);
        }

        public override void HandleBackToGameButtonClick() {
            gameSettingPanel.SetActive(false);
        }

        public override void HandleBackToMainButton() {
            Debug.Log("Back to Main");
        }
    }
}