using System.Collections.Generic;
using RiskSrc.Controller.Command;
using RiskSrc.Controller.Command.Multiple;
using RiskSrc.Controller.Command.Unique;
using RiskSrc.Controller.HumanPlayer.Phase;
using RiskSrc.Model;
using RiskSrc.View.Component;

namespace RiskSrc.Controller.HumanPlayer.Map {
    /*
     * The class is the Map controller in the attack phase
     */
    public class AttackPhaseControllerCore: AbstractHumanPlayerPhaseControllerCore{
        
        private int numberOfArmies;
        private string toCountryName;
        private string fromCountryName;

        private readonly RiskViewConfigurePanel attackPanel;
        

        private readonly RiskCommandInvoker commandInvoker;
        private readonly RiskDataManager dataManager;

        public AttackPhaseControllerCore(string playerName, RiskViewConfigurePanel panel) 
            : base(playerName){
            attackPanel = panel;
            dataManager = RiskDataManager.instance;
            commandInvoker = RiskCommandInvoker.instance;
        }

        protected override void WorkCore() {
            attackPanel.SliderChange += HandleScrollValueChange;
        }
        
        protected override void StopCore() {
            attackPanel.SliderChange -= HandleScrollValueChange;
        }

        /*Handle click event, it will highlight the clicked country or add a trajectory between two country*/
        protected override void HandleOnAreaClick(string clickedCountryName) {
            if (fromCountryName == clickedCountryName) return;
            
            
            if (fromCountryName == null) {// Guard the attacker is decided else
                if(dataManager.GetOwnerName(clickedCountryName) != playerName) return;
                if (dataManager.GetAreaActiveArmiesSize(clickedCountryName) == 0) return;
                fromCountryName = clickedCountryName;
                commandInvoker.ExecuteExclusiveCommand(new HighlightCommand(fromCountryName));
                return;
            }

            var attackerNeighboursNameList = dataManager.GetAreaNeighboursNameList(fromCountryName);
            if (!attackerNeighboursNameList.Contains(clickedCountryName)) return;
            if (dataManager.GetOwnerName(clickedCountryName) != playerName) { // If the country belong to the player 
                attackPanel.Title = "Attack";
                attackPanel.Limitation = dataManager.GetAreaActiveArmiesSize(fromCountryName);
                attackPanel.ConfirmButtonClick += ConfirmAttack;
                attackPanel.CancelButtonClick += CancelAttack;
            }
            else {
                attackPanel.Title = "Move";
                attackPanel.Limitation = dataManager.GetAreaActiveArmiesSize(fromCountryName);
                attackPanel.ConfirmButtonClick += ConfirmMove;
                attackPanel.CancelButtonClick += CancelMove;
            }
            attackPanel.Show();
        }

        /*Handle the cursor in country movement event*/
        protected override void HandleOnAreaEnter(string countryName) {
            if (fromCountryName == null) return;
            var neighboursNameList = dataManager.GetAreaNeighboursNameList(fromCountryName);
            if (neighboursNameList.Contains(countryName)) {
                toCountryName = countryName;
                var command = new MapAttackPreviewCommand(fromCountryName, countryName);
                commandInvoker.ExecuteExclusiveCommand(command);
            }
            else {
                commandInvoker.RollbackExclusiveCommand<MapAttackPreviewCommand>();
            }
        }

        private void ConfirmAttack() {
            if (numberOfArmies == 0) return;
            attackPanel.ConfirmButtonClick -= ConfirmAttack;
            attackPanel.CancelButtonClick -= CancelAttack;
            commandInvoker.ExecuteMultipleCommand(
                playerName, new AttackCommand(from: fromCountryName, to: toCountryName, size: numberOfArmies)
            );
            numberOfArmies = 0;
            attackPanel.Hide();
            HandleOnEscPress();
            HandleOnEscPress();
        }

        private void CancelAttack() {
            attackPanel.ConfirmButtonClick -= ConfirmAttack;
            attackPanel.CancelButtonClick -= CancelAttack;
            attackPanel.Hide();
        }

        private void ConfirmMove() {
            if (numberOfArmies == 0) return;
            attackPanel.ConfirmButtonClick -= ConfirmMove;
            attackPanel.CancelButtonClick -= CancelMove;
            commandInvoker.ExecuteMultipleCommand(
                playerName, new MoveCommand(from: fromCountryName,to: toCountryName,size: numberOfArmies)
            );
            numberOfArmies = 0;
            attackPanel.Hide();
            HandleOnEscPress();
            HandleOnEscPress();
        }
        
        
        private void CancelMove() {
            attackPanel.ConfirmButtonClick -= ConfirmMove;
            attackPanel.CancelButtonClick -= CancelMove;
            attackPanel.Hide();
        }

        private void HandleScrollValueChange(int change) {
            numberOfArmies = change;
        }

        protected override void HandleOnEscPress() {
            //TODO Complete the logic;
            if (commandInvoker.RollbackExclusiveCommand<MapAttackPreviewCommand>()) {
                toCountryName = null;
            }
            else if (commandInvoker.RollbackExclusiveCommand<HighlightCommand>()) {
                fromCountryName = null;
            }
        }

    }
}