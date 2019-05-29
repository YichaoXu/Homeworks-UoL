using System;
using RiskSrc.Model.Data;
using RiskSrc.Model.IO;

namespace RiskSrc.Model{
    public class RiskLogicCore {

        private static RiskLogicCore _instance;
        public static RiskLogicCore instance => _instance ?? (_instance = new RiskLogicCore());

        private readonly RiskGameDataModel dataModel; 

        private RiskLogicCore() {
            dataModel = RiskGameDataModel.instance;
        }
        
        public void SelectFirstTerritory(string playerName, string areaName) {
            dataModel.SetOwner(areaName, playerName);
        }

        public bool Reinforce(string toAreaName, int sizeOfRecruits) {
            var area = dataModel.GetArea(toAreaName);
            area.ActiveArmiesSize += sizeOfRecruits;
            return true;
        }

        public int GetPlayerInitialArmiesSize(string playerName) {
            var player = dataModel.GetPlayer(playerName);
            var ruleSettingFile = JsonFile.GetJsonFile(FilePath.RuleSettingAbsPath);
            return ruleSettingFile.Read()["InitiatedArmiesSize"][player.Type];
        }
        public int GetPlayerRecruitSize(string playerName) {
            var basicRecruitNum = dataModel.GetPlayer(playerName).BasicRecruit;
            
            var playerTerritories = dataModel.GetAreasListInTerritory(playerName);
            var territoryBonus = playerTerritories.Count / 3;
            var continentBonus = 0;
            dataModel.GetAllContinentData().ForEach(continentData => {
                var continentAreaList = dataModel.GetContinentAreasList(continentData.Name);
                var isTheContinentControlledByPlayer =
                    continentAreaList.TrueForAll(area=>playerTerritories.Contains(area));
                if (isTheContinentControlledByPlayer)
                    continentBonus += continentData.Bonus;
            });

            return basicRecruitNum + territoryBonus + continentBonus;
        }
        
        public bool Attack(string attackerAreaName, string defenderAreaName, int attackerSize) {
            var attackerArea = dataModel.GetArea(attackerAreaName);
            var defenderArea = dataModel.GetArea(defenderAreaName);
            var defenderSize = defenderArea.TotalArmiesSize;
            var randomNum = 0.75 * new Random().Next(1,2);
            var attackerBattleDamage = (int)Math.Ceiling(0.8 * randomNum* defenderSize);
            var defenderBattleDamage = (int)Math.Ceiling(0.6 * randomNum* attackerSize);
            var attackerLeft = attackerSize > attackerBattleDamage ?
                attackerSize - attackerBattleDamage : 0;
            var defenderLeft = defenderSize > defenderBattleDamage ? 
                defenderSize - defenderBattleDamage : 0;

            if (defenderLeft > 0) { // If defender win
                attackerArea.ActiveArmiesSize -= attackerSize;
                attackerArea.InactiveArmiesSize += attackerLeft;
                defenderArea.ActiveArmiesSize = 0;
                defenderArea.InactiveArmiesSize = defenderLeft;
                return false;
            }
            else { // If attacker win
                attackerArea.ActiveArmiesSize -= attackerSize;
                defenderArea.InactiveArmiesSize = attackerLeft;
                defenderArea.ActiveArmiesSize = 0;
                var attacker = dataModel.GetOwner(attackerAreaName);
                dataModel.SetOwner(defenderAreaName, attacker.Name);
                return true;
            }
        }
        
        public bool Move(string fromName, string toName, int armiesSize) {
            var fromArea = dataModel.GetArea(fromName);
            var toArea = dataModel.GetArea(toName);
            fromArea.ActiveArmiesSize -= armiesSize;
            toArea.InactiveArmiesSize += armiesSize;
            return true;
        }

        public void EndPlayerTurn(string playerName) {
            var areasList = dataModel.GetAreasListInTerritory(playerName);
            areasList.ForEach(area => {
                area.ActiveArmiesSize += area.InactiveArmiesSize;
                area.InactiveArmiesSize = 0;
            });
        }

        public bool isPlayerGameOver(string playerName) {
            return dataModel.GetAreasListInTerritory(playerName).Count == 0;
        }
    }
}
