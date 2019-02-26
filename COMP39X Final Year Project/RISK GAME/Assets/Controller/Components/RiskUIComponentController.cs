using System;
using UnityEngine;

/*
 * Thees Classes are the controller of the game Ui components
 * such as button, panel, scrollbar in different phase;
 */

namespace Risk.Controller.UIComponent {
    /*
     * The class is the Ui components controller in the initiate phase
     */
    public class UIComponentInitiateController : AbstractUIComponentController {
        /*Constructor*/
        public UIComponentInitiateController(UnityUIComponents uiComponents):base(uiComponents) {
            gamePhaseTitle.text = "INITIATE";
        }
        public UIComponentInitiateController(AbstractUIComponentController previous) : base(previous) {
            gameTurnTitle.text = "INITIATE";
            if (gameTurnTitle == null) return;
            currentTurnNum += 1;
            gameTurnTitle.text = "TURN " + currentTurnNum;
        }
        /*Methods*/
        public override void HandleOnCountryClick(string countryName) { /*Do Nothing*/ }
    }
    
    /*
     * The class is the Ui components controller in the attack phase
     */
    public class UIComponentAttackController : AbstractUIComponentController {
        /*Constructor*/
        public UIComponentAttackController(AbstractUIComponentController previous) : base(previous) {
            gamePhaseTitle.text = "ATTACK";
        }
        /*Methods*/
        public override void HandleOnCountryClick(string countryName) { /*TODO: HANDLE THE CLICK EVENT*/ }
    }
    
    /*
     * The class is the Ui components controller in the Recruit phase
     */
    public class UIComponentRecruitController : AbstractUIComponentController {

        /*The variables are used to update Armies size on the country*/
        private string clickedCountryName;
        private int recruitSize;
        public UIComponentRecruitController(AbstractUIComponentController previous) : base(previous) {
            gamePhaseTitle.text = "RECRUIT";
        }

        public override void HandleOnCountryClick(string countryName) {
            clickedCountryName = countryName;
            deployingPanel.SetActive(true);
        }
        /*
         * Update the armies size on the deploying canvas 
         */
        public void updateArmiesSize(float armiesSize) {
            var playerTotalRecruitSize = (float)modelDelegate.GetPlayerRecruitSize();
            recruitSize = (int)Math.Ceiling(playerTotalRecruitSize * armiesSize);
            deployingTitle.text = "Size: " + recruitSize;
        }

        public void HandleConfirmButtonClick() {
            modelDelegate.DeployArmies(clickedCountryName, recruitSize);
            deployingPanel.SetActive(false);
            clickedCountryName = "";
            recruitSize = 0;
        }
    }
    
    
}