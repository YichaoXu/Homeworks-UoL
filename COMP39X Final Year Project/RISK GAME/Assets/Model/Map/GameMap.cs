using System;
using System.Collections.Generic;
using Assets.Model;
using SimpleJSON;

namespace Risk.Model.Rule {

    public class GameMap {
        
        public readonly List<Continent> continentsList;
        public readonly List<Country> countriesList;
        private readonly byte[][] countriesAdjacencyMatrix;

        public GameMap(List<JSONNode> continentRawDataList, List<JSONNode> countryRawDataList) {
            continentsList = new List<Continent>();
            continentRawDataList.ForEach((continentRawData)
                => continentsList.Add(new Continent(continentRawData))
            );
            countriesList = new List<Country>();
            var countriesNum = countryRawDataList.Count;
            countriesAdjacencyMatrix = new byte[countriesNum][];
            for (int i=0; i<countriesNum; i++) {
                countriesAdjacencyMatrix[i] = new Byte[countriesNum];
                for (int j = 0; j < countriesNum; j++) 
                    countriesAdjacencyMatrix[i][j] = 0;
            }
            countryRawDataList.ForEach((countryRawData) => {
                countriesList.Add(new Country(countryRawData));
                var countryId = countryRawData["id"];
                var tmpNeighbourIdArray = countryRawData["neighbours"].AsArray;
                foreach (var neighbourIndex in tmpNeighbourIdArray) {
                    var neighbourIndexNum = neighbourIndex.Value.AsInt;
                    countriesAdjacencyMatrix[countryId][neighbourIndexNum] = 1;
                }
            });
        }

        public List<string> GetCountryNeighboursName(string countryName) {
            var tmpRst = new List<string>();
            var countryId = GetCountryIndex(countryName);
            if (countryId < 0) return tmpRst;
            var neighboursArray = countriesAdjacencyMatrix[countryId];
            for (int i = 0; i < neighboursArray.Length; i++) 
                if (neighboursArray[i] == 1) tmpRst.Add(countriesList[i].name);
            return tmpRst;
        }

        public int GetCountryArmiesSize(string countryName) {
            var countryIndex = GetCountryIndex(countryName);
            return countriesList[countryIndex].armiesSize;
        }
        public bool DeployArmies(string countryName, int number) {
            var countryIndex = GetCountryIndex(countryName);
            if (countryIndex < 0) {
                return false;
            }
            else {
                countriesList[countryIndex].Reinforcement(number);
                return true;
            }
        }

        private int GetCountryIndex(string countryName) {
            foreach (var country in countriesList) 
                if (country.name == countryName) return country.id;
            return -1;
        }

    }
}
