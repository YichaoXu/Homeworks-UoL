using System.Collections.Generic;
using System.Linq;
using RiskSrc.Model.IO;
using UnityEngine;

namespace RiskSrc.Model.Data{

    public class RiskGameDataModel {
        
        private static RiskGameDataModel _instance;
        public static RiskGameDataModel instance => _instance ?? (_instance = new RiskGameDataModel());
        
        //1. Geography Data
        //1.1 Data Model for the areas;
        private readonly List<AreaData> areasList;
        private readonly RelationMatrix areasAdjacencyMatrix;
        
        //1.2 Data Model for the continents;
        private readonly List<ContinentData> continentsList;
        private readonly RelationMatrix continentAreasMatrix;
        
        //2. Player Data
        private readonly List<PlayerData> playersList;
        private readonly RelationMatrix playerTerritoriesMatrix;

        private RiskGameDataModel() {
            //1. Initiate Area Data Model;
            var AreaDataFile= JsonFile.GetJsonFile(FilePath.AreasDataAbsPath);
            var areaDataConverter = new RiskDataConverter<AreaData>(AreaDataFile.Read().AsArray);
            areasList = areaDataConverter.GetTargetDataList();
            var areaNum = areasList.Count;
            areasAdjacencyMatrix = areaDataConverter.GetRelationMatrix(
                with: "Neighbours", row:areaNum, column:areaNum
            );
            //2. Initiate the Continent Data Model
            var continentDataFile = JsonFile.GetJsonFile(FilePath.ContinentsDataAbsPath);
            var continentDataConverter = new RiskDataConverter<ContinentData>(continentDataFile.Read().AsArray);
            continentsList = continentDataConverter.GetTargetDataList();
            var continentNum = continentsList.Count;
            continentAreasMatrix = continentDataConverter.GetRelationMatrix(
                with: "Countries", row:continentNum, column:areaNum
            );
            //3. Initiate the Player Data model
            var playerDataFile = JsonFile.GetJsonFile(FilePath.PlayersDataAbsPath);
            var playerDataConverter = new RiskDataConverter<PlayerData>(playerDataFile.Read().AsArray);
            playersList = playerDataConverter.GetTargetDataList();
            var playerNum = playersList.Count;
            playerTerritoriesMatrix = playerDataConverter.GetRelationMatrix(
                with: "Countries", row:playerNum, column:areaNum
            );
        }

        public List<AreaData> GetAllAreasData() {
            return areasList;
        }
        
        public AreaData GetArea(string areaName) {
            return areasList.Find(area => area.Name == areaName);
        }
        
        public List<AreaData> GetNeighboursList(string areaName) {
            var area = GetArea(areaName);
            var neighboursIdList = 
                areasAdjacencyMatrix.GetColumnIndexesWhichExistRelation(withRow: area.ID);
            var neighbourAreas = 
                neighboursIdList.Select(areaID => areasList[areaID]);
            return neighbourAreas.ToList();
        }
        
        public List<ContinentData> GetAllContinentData() {
            return continentsList;
        }
        
        public ContinentData GetContinentData(string name) {
            return continentsList.Find(continent => continent.Name == name);
        }
        
        public List<AreaData> GetContinentAreasList(string continentName) {
            var continentID = GetContinentData(continentName).ID;
            var areasIdList = 
                continentAreasMatrix.GetColumnIndexesWhichExistRelation(withRow: continentID);
            var continentAreas = 
                areasIdList.Select(areaID => areasList[areaID]);
            return continentAreas.ToList();
        }
        
        public List<PlayerData> GetAllPlayersData() {
            return playersList;
        }
        
        public PlayerData GetPlayer(string name) {
            return playersList.Find(player => player.Name == name);
        }

        public List<AreaData> GetAreasListInTerritory(string ofPlayerName) {
            var playerData = GetPlayer(ofPlayerName);
            var areasIdList =
                playerTerritoriesMatrix.GetColumnIndexesWhichExistRelation(withRow: playerData.ID);
            var playerAreas =
                areasIdList.Select(areaID => areasList[areaID]);
            return playerAreas.ToList();
        }

        public PlayerData GetOwner(string areaName) {
            var area = GetArea(areaName);
            var playerIdList = 
                playerTerritoriesMatrix.GetRowIndexesWhichExistRelation(withColumn: area.ID);
            if (playerIdList.Count == 0) return null;
            return playersList[playerIdList[0]];
        }

        public void SetOwner(string areaName, string playerName) {
            var area = GetArea(areaName);
            var previousOwner = GetOwner(areaName);
            if(previousOwner != null )
                playerTerritoriesMatrix.SetRelation(previousOwner.ID, area.ID, false);
            var newOwner = GetPlayer(playerName);
            if(newOwner != null)
                playerTerritoriesMatrix.SetRelation(newOwner.ID, area.ID, true);
        }
    }
}
