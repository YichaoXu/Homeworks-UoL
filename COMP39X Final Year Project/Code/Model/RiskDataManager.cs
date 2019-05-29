using System.Collections.Generic;
using System.Linq;
using RiskSrc.Model.Data;

namespace RiskSrc.Model {
    public class RiskDataManager {
        private static RiskDataManager _instance;

        private readonly RiskGameDataModel dataModel;

        private RiskDataManager() {
            dataModel = RiskGameDataModel.instance;
        }

        public static RiskDataManager instance => _instance ?? (_instance = new RiskDataManager());

        public List<string> GetPlayerAreaNameList(string playerName) {
            var playerAreasList = dataModel.GetAreasListInTerritory(playerName);
            return playerAreasList.Select(area => area.Name).ToList();
        }

        public List<string> GetContinentAreasNameList(string continentName) {
            var continentAreasList = dataModel.GetContinentAreasList(continentName);
            return continentAreasList
                .Select(continent => continent.Name)
                .ToList();
        }

        public int GetAreaActiveArmiesSize(string areaName) {
            var areaList = dataModel.GetAllAreasData();
            var area = areaList.Find(anyArea => anyArea.Name == areaName);
            return area.ActiveArmiesSize;
        }       
        
        public int GetAreaTotalArmiesSize(string areaName) {
            var areasList = dataModel.GetAllAreasData();
            var area = areasList.Find(anyArea => anyArea.Name == areaName);
            return area.TotalArmiesSize;
        }

        public Dictionary<string, int> GetAllAreasTotalArmiesSize() {
            var tmpDic = new Dictionary<string, int>();
            var areasList = dataModel.GetAllAreasData();
            areasList.ForEach(area => {
                tmpDic.Add(area.Name, area.TotalArmiesSize);
            });
            return tmpDic;
        }

        public List<string> GetAreaNeighboursNameList(string areaName) {
            var neighbourList = dataModel.GetNeighboursList(areaName);
            return neighbourList.Select(area => area.Name).ToList();
        }

        public List<string> GetAllAreasNameList() {
            return dataModel.GetAllAreasData().Select(areaData => areaData.Name).ToList();
        }

        public string GetOwnerName(string areaName) {
            var ownerName = dataModel.GetOwner(areaName)?.Name;
            if (ownerName != null && ownerName.Equals("")) return null; 
            return ownerName;
        }
    }
}