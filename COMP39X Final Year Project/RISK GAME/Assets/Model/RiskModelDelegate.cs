using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Risk.Model.Data;
using Risk.Model.Rule;


namespace Risk.Model {
    public class RiskModelDelegate : ModelDelegateInterface {

        private static RiskModelDelegate _instance;
        public static RiskModelDelegate instance {
            get { return _instance ?? (_instance = new RiskModelDelegate()); }
        }
        
        private readonly GameMap gameMap; 
        private readonly LocalDataManager dataManager;
        private RiskModelDelegate() {
            dataManager = new LocalDataManager("countryData.json");
            var countryRawDataList = dataManager.GetCountriesRawDataList();
            var continentRawDataList = dataManager.GetContinentsRawDataList();
            gameMap = new GameMap(continentRawDataList,countryRawDataList);
        }

        public bool DeployArmies(string countryName,int number) {
            //TODO: Identify the number of the size new armies of the player;
            playerRecruitSize -= number;
            return gameMap.DeployArmies(countryName,number);
        }

        private int playerRecruitSize = 5;
        public int GetPlayerRecruitSize() {
            return playerRecruitSize;
        }

        public bool Attack(int size, string countryName) {
            throw new System.NotImplementedException();
        }

        public List<string> GetAllCountriesNameList() {
            return gameMap.countriesList.Select((eachCountry) => eachCountry.name).ToList();
        }

        public int GetCountryArmiesSize(string countryName) {
            return gameMap.GetCountryArmiesSize(countryName);
        }
        
        public Dictionary<string, int> GetAllCountriesArmiesSize() {
            var tmpDic = new Dictionary<string, int>();
            gameMap.countriesList.ForEach((country) => {
                tmpDic.Add(country.name,country.armiesSize);
            });
            return tmpDic;
        }

        public List<string> GetCountryNeighboursNameList(string countryName) {
            return gameMap.GetCountryNeighboursName(countryName);
        }
    }
}