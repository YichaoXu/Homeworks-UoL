using RiskSrc.Model;
using RiskSrc.Model.IO;
using RiskSrc.View.Map;
using UnityEngine;

namespace RiskSrc.Controller.Command.Multiple {
    
    public class SelectFirstCommand: AbstractRiskMultipleCommand {
        
        private readonly RiskLogicCore logicCore;
        private readonly RiskDataManager dataManager;

        private readonly int playerInitialArmiesSize;
        private readonly string currentPlayerName;
        private readonly string selectedTerritoryName;


        public override string Description => "selects first territory, " + selectedTerritoryName;

        public SelectFirstCommand(string playerName, string territoryName) {
            currentPlayerName = playerName;
            selectedTerritoryName = territoryName;
            
            logicCore = RiskLogicCore.instance;
            playerInitialArmiesSize = logicCore.GetPlayerInitialArmiesSize(playerName);

        }

        protected override bool ViewUpdateCore() {
            var colorSetting = JsonFile.GetJsonFile(FilePath.DecorationSettingAbsPath).Read();
            var playerColor = colorSetting["Players"][currentPlayerName].AsColor();
            MapSurfacePainter.instance.SetAreaColor(
                name:selectedTerritoryName, 
                color:playerColor, 
                priority:AreaColorPriority.Player
            );
            return true;
        }

        protected override bool ModelUpdateCore() {
            logicCore.SelectFirstTerritory(currentPlayerName, selectedTerritoryName);
            logicCore.Reinforce(selectedTerritoryName, playerInitialArmiesSize);
            MapAreaLabeler.instance.SetLabel(
                onAreaName: selectedTerritoryName,
                labelStr: playerInitialArmiesSize.ToString()
            );
            return true;
        }

        protected override bool ViewUpdateRollbackCore() {
            throw new System.NotImplementedException();
        }

        protected override bool ModelUpdateRollbackCore() {
            throw new System.NotImplementedException();
        }
    }
}