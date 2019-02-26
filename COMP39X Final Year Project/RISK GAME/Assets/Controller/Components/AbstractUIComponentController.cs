using Risk.Model;
using UnityEngine;
using UnityEngine.UI;


namespace Risk.Controller.UIComponent {
    /*
     * The abstract class for the Ui controller class;
     * It constrains the behaviours and properties of
     * the Ui Components controller 
     */
    public abstract class AbstractUIComponentController {
        
        /*Protected variables*/
        /*Unity GameObject*/
        protected Text gameTurnTitle;
        protected Text gamePhaseTitle;
        protected Text deployingTitle;
        protected GameObject gameResultPanel;
        protected GameObject gameSettingPanel;
        protected GameObject deployingPanel;
        /*Variables*/
        protected int currentTurnNum;
        protected ModelDelegateInterface modelDelegate;

        /*Constructors*/
        
        /*
         * constructor for the AbstractUIComponentController.
         * parameters: UnityUIComponents, the structure includes
         * all variable will be used to initiate the UIComponentController
         */
        protected AbstractUIComponentController(UnityUIComponents uiComponents) {
            currentTurnNum = 1;
            gameTurnTitle = uiComponents.gameTurnTitle;
            gamePhaseTitle = uiComponents.gamePhaseTitle;
            deployingTitle = uiComponents.deployingTitle;
            gameResultPanel = uiComponents.gameResultPanel;
            gameSettingPanel = uiComponents.gameSettingPanel;
            deployingPanel = uiComponents.deployingPanel;
            modelDelegate = RiskModelDelegate.instance;
        }
        protected AbstractUIComponentController(AbstractUIComponentController previous) {
            currentTurnNum = previous.currentTurnNum;
            gameTurnTitle = previous.gameTurnTitle;
            gamePhaseTitle = previous.gamePhaseTitle;
            deployingTitle = previous.deployingTitle;
            gameResultPanel = previous.gameResultPanel;
            gameSettingPanel = previous.gameSettingPanel;
            deployingPanel = previous.deployingPanel;
            modelDelegate = previous.modelDelegate;
        }

        /*Handle the event occuring when user clicks the "Back to Main" Button*/
        public void HandleBackToDesktopButton() {
            Application.Quit();
        }
        
        /*Handle the event occuring when user clicks the "Setting" Button*/
        public void HandleSettingButtonClick() {
            gameSettingPanel.SetActive(true);
        }
        /*Handle the event occuring when user clicks the "Exit" Button*/
        public void HandleExitButtonClick() {
            gameResultPanel.SetActive(true);
        }
        /*Handle the event occuring when user clicks the "BackToGame" Button*/
        public void HandleBackToGameButtonClick() {
            gameSettingPanel.SetActive(false);
        }
        /*Handle the event occuring when user clicks the "BackToMain" Button*/
        public void HandleBackToMainButton() {
            Debug.Log("Back to Main");
        }
        /*Handle the event occuring when user clicks the country on the map*/
        public abstract void HandleOnCountryClick(string countryName);
        
    }

    public struct UnityUIComponents {
        public Text gameTurnTitle;
        public Text gamePhaseTitle;
        public Text deployingTitle;
        public GameObject gameResultPanel;
        public GameObject gameSettingPanel;
        public GameObject deployingPanel;
    }
}