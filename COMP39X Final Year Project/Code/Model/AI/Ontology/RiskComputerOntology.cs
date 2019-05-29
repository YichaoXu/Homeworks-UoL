using System.Collections.Generic;
using System.Linq;
using RiskSrc.Model.Data;

namespace RiskSrc.Model.AI.Ontology {
    
    public class RiskComputerOntology {
        private readonly RiskGameDataModel dataModel;
        
        private readonly string playerName;
        private readonly Dictionary<AreaData, BalanceNode> ontologyCore;

        public List<BalanceNode> FrontLine => (
            from node in ontologyCore.Values
            where node.isFrontLine()
            orderby node.ArmiesBalance descending
            select node
        ).ToList();
        
        public List<BalanceNode> HomeFront => (
            from node in ontologyCore.Values
            where ! node.isFrontLine()
            orderby node.ArmiesBalance descending
            select node
        ).ToList();
        
        public RiskComputerOntology(string playerName) {
            this.playerName = playerName;
            dataModel = RiskGameDataModel.instance;
            ontologyCore = new Dictionary<AreaData, BalanceNode>();
        }
        
        public void BuildGraph(){
            var playerTerritoriesList = dataModel.GetAreasListInTerritory(playerName);
            if(playerTerritoriesList.Count == ontologyCore.Count) return;
            //Add new neighbours
            playerTerritoriesList.ForEach(playerTerritoryData => {
                if(ontologyCore.ContainsKey(playerTerritoryData)) return;
                var nearbyAreaDataList = dataModel.GetNeighboursList(playerTerritoryData.Name);
                var nearbyTerritoriesNodes = (
                    from nearbyAreaData in nearbyAreaDataList
                    where ontologyCore.ContainsKey(nearbyAreaData)
                    select ontologyCore[nearbyAreaData]
                ).ToList();
                var newNode = new BalanceNode(playerTerritoryData);
                newNode.ConnectWithNearbyTerritories(nearbyTerritoriesNodes);
                ontologyCore[playerTerritoryData] = newNode;
            });
            //Delete the neighbours which is invalid
            var removedNodesList = new List<AreaData>();
            foreach (var ontologyPair in ontologyCore) {
                var ontologyAreaData = ontologyPair.Key;
                if(playerTerritoriesList.Contains(ontologyAreaData)) continue;
                ontologyCore[ontologyAreaData].DisconnectWithNearbyTerritories();
                removedNodesList.Add(ontologyAreaData);
            }
            removedNodesList.ForEach(data=>ontologyCore.Remove(data));
            
            HomeFront.ForEach(homeFrontNode => homeFrontNode.PrepareForLevelBoardCast());
            FrontLine.ForEach(frontLineNode => frontLineNode.LevelBoardCast());
        }
        

        public void UpdateBalance() {
            var level = 0;
            List<BalanceNode> nodesInSameLevel;
            do {
                nodesInSameLevel = (
                    from node in ontologyCore
                    where node.Value.CurrentLevel == level
                    select node.Value
                ).ToList();
                
                nodesInSameLevel.ForEach(node => {
                    node.PrepareForBalanceBoardCast();
                    node.BalanceBoardCast();
                });
                level += 1;
            } while (nodesInSameLevel.Any());
        }
    }
}