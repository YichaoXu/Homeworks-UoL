using System;
using System.Collections.Generic;
using Assets.Model;
using NUnit.Framework.Internal;
using Risk.Model.Data;
using SimpleJSON;

namespace Risk.Model.Rule {

    public class GameMap {
        
        private readonly List<Continent> continentsList;
        private readonly List<Country> countriesList;
        private readonly byte[,] countriesAdjacencyMatrix;

        public GameMap(List<JSONNode> continentRawDataList, List<JSONNode> countryRawDataList) {
            continentsList = new List<Continent>();
            continentRawDataList.ForEach((continentRawData)
                => continentsList.Add(new Continent(continentRawData))
            );

            countriesList = new List<Country>();
            var countriesNum = countryRawDataList.Count;
            countriesAdjacencyMatrix = new byte[countriesNum,countriesNum];
            for (int i=0; i<countriesNum; i++) for(int j=0; j<countriesNum; j++) 
                countriesAdjacencyMatrix[i, j] = 0;
            countryRawDataList.ForEach((countryRawData) => {
                countriesList.Add(new Country(countryRawData));
                var countryId = countryRawData["id"];
                var tmpNeighbourIdArray = countryRawData["neighbours"].AsArray;
                foreach (var neighbourIndex in tmpNeighbourIdArray) {
                    var neighbourIndexNum = neighbourIndex.Value.AsInt;
                    countriesAdjacencyMatrix[countryId, neighbourIndexNum] = 1;
                }
            });
        }

        public List<string> GetCountryNeighboursName(string countryName) {
            var tmpRst = new List<string>();
            var countryId = GetCountryIndex(countryName);
            if (countryId < 0) return tmpRst;
            for (int i = 0; i < countriesAdjacencyMatrix.Length; i++) {
                if (countriesAdjacencyMatrix[countryId, i] != 1) continue;
                tmpRst.Add(countriesList[i].name);
            }
            return tmpRst;
        }

        private int GetCountryIndex(string countryName) {
            foreach (var country in countriesList) 
                if (country.name == countryName) return country.id;
            return -1;
        }
    }
}
