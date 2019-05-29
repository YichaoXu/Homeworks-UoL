using System;
using RiskSrc.Controller.Command;
using RiskSrc.Controller.Command.Multiple;
using RiskSrc.Controller.HumanPlayer.Phase;
using RiskSrc.Model;
using RiskSrc.View.Component;

namespace RiskSrc.Controller.HumanPlayer.Map {
    /*
     * The class is the Map controller in the initiate phase
     */
    public class InitiatePhaseControllerCore: AbstractHumanPlayerPhaseControllerCore {
        private readonly RiskViewDialog notificationDialog;

        private readonly RiskDataManager dataManager;
        private readonly RiskCommandInvoker commandInvoker;
        
        private int numberOfArmies;
        private int numberOfMaxArmies;
        private string selectedCountryName;

        public delegate void InitiationFinishDelegate();

        public event InitiationFinishDelegate InitiateFinishEvent;

        public InitiatePhaseControllerCore(string playerName, RiskViewDialog riskViewDialog) 
            :base(playerName){
            notificationDialog = riskViewDialog;
            dataManager = RiskDataManager.instance;
            commandInvoker = RiskCommandInvoker.instance;
        }

        protected override void WorkCore() {
            notificationDialog.Title = "Notification";
            notificationDialog.Content = "Please, select your first territory!";
            notificationDialog.Show();
            notificationDialog.OnConfirmButtonClick += StartToSelectCountry;
        }

        protected override void StopCore() { 
            notificationDialog.Hide();
        }

        private void StartToSelectCountry() {
            notificationDialog.Hide();
            notificationDialog.OnConfirmButtonClick += ConfirmSelectedCountry;
        }
        private void ConfirmSelectedCountry() {
            commandInvoker.ExecuteMultipleCommand(
                playerName, new SelectFirstCommand(playerName, selectedCountryName)
            );
            notificationDialog.Hide();
            InitiateFinishEvent?.Invoke();
        }

        /*In this phase, we do not need the class to do anything else*/
        protected override void HandleOnAreaClick(string clickedCountryName) {
            if (dataManager.GetOwnerName(clickedCountryName) != null) return;
            selectedCountryName = clickedCountryName;
            notificationDialog.Title = clickedCountryName;
            notificationDialog.Content = "Select " + clickedCountryName + " as your first territory.";
            notificationDialog.Show();
        }
        
        protected override void HandleOnAreaEnter(string countryName) {}
        
        protected override void HandleOnEscPress() {}
    }
}