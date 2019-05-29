using System.Collections.Generic;
using RiskSrc.Controller.Command;
using RiskSrc.Controller.Command.Multiple;
using RiskSrc.Controller.Command.Unique;
using RiskSrc.Controller.HumanPlayer.Phase;
using RiskSrc.Model;
using RiskSrc.View.Component;

namespace RiskSrc.Controller.HumanPlayer.Map {
    /*
     * The class is the Map controller in the Recruit phase
     */
    public class RecruitPhaseControllerCore : AbstractHumanPlayerPhaseControllerCore {
        private readonly RiskCommandInvoker commandInvoker;
        private readonly RiskViewConfigurePanel deployPanel;

        private int numberOfReinforcement;
        private int maxNumberOfReinforcement;
        private string nameOfReinforcedCountryName;
        private List<string> playerCountryNameList;
        
        public RecruitPhaseControllerCore(string playerName, RiskViewConfigurePanel panel) 
            : base(playerName){
            deployPanel = panel;
            commandInvoker = RiskCommandInvoker.instance;
        }
        
        protected override void WorkCore() {
            deployPanel.SliderChange += ReinforceSizeChange;
            deployPanel.ConfirmButtonClick += ConfirmToDeploy;
            deployPanel.CancelButtonClick += CancelToDeploy;
            
            var dataManager = RiskDataManager.instance;
            var riskLogicCore = RiskLogicCore.instance;
            maxNumberOfReinforcement = riskLogicCore.GetPlayerRecruitSize(playerName);
            playerCountryNameList = dataManager.GetPlayerAreaNameList(playerName);
        }

        protected override void StopCore() {
            deployPanel.Hide();
            HandleOnEscPress();
        }

        protected override void HandleOnAreaClick(string countryName) {
            if(!playerCountryNameList.Contains(countryName)) return;
            nameOfReinforcedCountryName = countryName;
            var highlightCommand = new HighlightCommand(countryName);
            commandInvoker.ExecuteExclusiveCommand(highlightCommand);
            deployPanel.Title = "Reinforcement";
            deployPanel.Limitation = maxNumberOfReinforcement;
            deployPanel.Show();
        }

        protected override void HandleOnAreaEnter(string countryName) {}

        protected override void HandleOnEscPress() {
            commandInvoker.RollbackExclusiveCommand<HighlightCommand>();
        }

        private void ReinforceSizeChange(int number) {
            numberOfReinforcement = number;
        }

        private void ConfirmToDeploy() {
            if (numberOfReinforcement == 0) return;
            if (nameOfReinforcedCountryName == null) return;

            commandInvoker.ExecuteMultipleCommand(
                playerName, new RecruitCommand(nameOfReinforcedCountryName,numberOfReinforcement)
            );
            maxNumberOfReinforcement -= numberOfReinforcement;
            nameOfReinforcedCountryName = null;
            numberOfReinforcement = 0;
            deployPanel.Hide();
        }

        private void CancelToDeploy() {
            nameOfReinforcedCountryName = null;
            numberOfReinforcement = 0;
            deployPanel.Hide();
        }
    }
}