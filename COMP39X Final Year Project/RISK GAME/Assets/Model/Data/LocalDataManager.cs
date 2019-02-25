using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleJSON;
using UnityEngine;

namespace Risk.Model.Data {
    public class LocalDataManager {
        private readonly JSONNode[] continentRawDataArray;
        private readonly JSONNode[] countriesRawDataArray;

        public LocalDataManager(string fileName) {
            var jsonFilePath = Path.Combine(Application.dataPath + "/Model/Data/", fileName);
            if (!File.Exists(jsonFilePath))
                Debug.LogError("File don't exist in " + jsonFilePath);
            var file = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read);
            var fileReader = new StreamReader(file);
            var jsonStr = fileReader.ReadToEnd();
            var jsonObject = JSON.Parse(jsonStr);
            
            var jsonContinentsDataList = jsonObject["continent"].AsArray;
            continentRawDataArray = new JSONNode[jsonContinentsDataList.Count];
            for (int i = 0; i < jsonContinentsDataList.Count; i++) 
                continentRawDataArray[i] = continentRawDataArray[i];
            
            var jsonCountriesRawDataList = jsonObject["countries"].AsArray;
            countriesRawDataArray = new JSONNode[jsonCountriesRawDataList.Count];
            foreach (var countryRawData in jsonCountriesRawDataList) {
                var jsonData = countryRawData.Value;
                countriesRawDataArray[jsonData["id"].AsInt] = jsonData;
            }
        }

        public List<JSONNode> GetCountriesRawDataList() {
            return countriesRawDataArray.ToList();
        }

        public List<JSONNode> GetContinentsRawDataList() {
            return continentRawDataArray.ToList();
        }
    }
}