using System.Collections.Generic;
using System.Linq;
using Risk.Model.Data;
using SimpleJSON;
using UnityEditor;
using UnityEngine;

namespace Risk.Model {
    public class RiskModelDelegate : ModelDelegateInterface {

        private static RiskModelDelegate _instance;
        
        public static RiskModelDelegate instance {
            get { return _instance ?? (_instance = new RiskModelDelegate()); }
        }

        private readonly LocalDataManager dataManager;
        private RiskModelDelegate() {
            dataManager = new LocalDataManager("countryData.json");
            
        }

        public bool DeployArmies(int number, string countryName) {
            throw new System.NotImplementedException();
        }

        public bool Attack(int size, string countryName) {
            throw new System.NotImplementedException();
        }

        public List<string> GetAllCountriesNameList() {
            throw new System.NotImplementedException();
        }

        bool ModelDelegateInterface.DeployArmies(int size, string toCountryName) {
            throw new System.NotImplementedException();
        }
        
        public List<string> GetCountryNeighboursNameList(string countryName) {
            List<JSONNode> countriesRawDataList = dataManager.GetCountriesRawDataList();
            var nameList = new List<string>();
            countriesRawDataList.ForEach((countryRawData) => {
                if (countryRawData == null) return;
                if (countryRawData["name"] != countryName) {return;}
                foreach (var index in countryRawData["neighbours"].AsArray) {
                    nameList.Add(countriesRawDataList[index.Value]["name"]);
                }

            });
            return nameList;
        }
    }
}