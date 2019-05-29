using System.Collections.Generic;
using System.Linq;
using RiskSrc.Controller;
using RiskSrc.Model.IO;
using SimpleJSON;
using UnityEngine;

namespace RiskSrc.Model {
    
    public class RiskInitiator {
        
        private readonly JSONNode playerData;
        private readonly JSONNode colorSetting;
        private readonly List<JSONNode> playerDefaultColorSettingList;
        
        
        public RiskInitiator() {
            playerData = JsonFile.GetJsonFile(FilePath.PlayersDataAbsPath).Read();
            colorSetting = JsonFile.GetJsonFile(FilePath.DecorationSettingAbsPath).Read();
            playerDefaultColorSettingList = colorSetting["Players"].Children.ToList();
            colorSetting.Remove("Players");
            
            var playerColorSettingObject = new JSONObject();
            foreach (var playerRawData in playerData) {
                var playerName = playerRawData.Value["Name"];
                var playerID = playerRawData.Value["ID"].AsInt;
                playerColorSettingObject.Add(playerName, playerDefaultColorSettingList[playerID]);
            }
            colorSetting["Players"] = playerColorSettingObject;
        }
        public bool CreateGamePlayer(string playerName, RiskPlayerType playerType) {
            var ruleSettingFile = JsonFile.GetJsonFile(FilePath.RuleSettingAbsPath);
            var maxPlayerSize = ruleSettingFile.Read()["MaxPlayerSize"].AsInt;
            if (playerData.Count > maxPlayerSize) return false; 
            var playerJsonData = new JSONObject();
            var id = playerData.Count;
            playerJsonData["ID"] = id;
            playerJsonData["Name"] = playerName;
            playerJsonData["Type"] = playerType.ToString();
            var ruleSetting = ruleSettingFile.Read();
            playerJsonData["Recruit"] = ruleSetting["BasicRecruitNumber"][playerType.ToString()];
            playerJsonData["Countries"] = new JSONArray();
            playerData.Add(playerJsonData);
            colorSetting["Players"].Add(playerName, playerDefaultColorSettingList[id]);
            return true;
        }

        public void SetPlayerColor(string playerName, Color color) {
            for (var index = 0; index < playerData.Count; index++) {
                if (playerData[index]["Name"] != playerName) continue;
                var colorJson = new JSONObject {
                    ["R"] = color.r, ["G"] = color.g, ["B"] = color.b
                };
                colorSetting["Players"][playerName] = colorJson;
                break;
            }
        }

        public void Finish() {
            JsonFile.GetJsonFile(FilePath.DecorationSettingAbsPath).Write(colorSetting);
            JsonFile.GetJsonFile(FilePath.PlayersDataAbsPath).Write(playerData);
        }
        
    }
}