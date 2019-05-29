using System.Collections.Generic;
using System.Linq;
using RiskSrc.Model.Data;
using UnityEngine;
using WPMF.PolygonClipping;

namespace RiskSrc.Model.AI.Ontology {
    public class BalanceNode {
        public int CurrentLevel { get; private set; }
        public int ArmiesBalance { get; private set; }
        public AreaData areaData => _areaData;
        public List<BalanceNode> NeighbourTerritoriesNodes => nearbyTerritoryNodeMap.Values.ToList();
        
        private readonly AreaData _areaData;
        private readonly Dictionary<AreaData, BalanceNode> nearbyTerritoryNodeMap;
        

        public BalanceNode(AreaData data) {
            _areaData = data;
            nearbyTerritoryNodeMap = new Dictionary<AreaData, BalanceNode>();
            
        }

        public void ConnectWithNearbyTerritories(List<BalanceNode> neighbourTerritoryNodeList) {
            neighbourTerritoryNodeList.ForEach(Connect);
        }
        
        private void Connect(BalanceNode withAreaNode) {
            if (nearbyTerritoryNodeMap.ContainsKey(withAreaNode._areaData)) return;
            nearbyTerritoryNodeMap[withAreaNode._areaData] = withAreaNode;
            withAreaNode.Connect(this);
        }
        
        public void DisconnectWithNearbyTerritories() {
            nearbyTerritoryNodeMap.Values.ToList().ForEach(Disconnect);
        }
        
        private void Disconnect(BalanceNode withAreaNode) {
            if (!nearbyTerritoryNodeMap.ContainsKey(withAreaNode._areaData)) return;
            nearbyTerritoryNodeMap.Remove(withAreaNode._areaData);
            withAreaNode.Disconnect(this);
        }
        
        public void LevelBoardCast(){
            var nearbyTerritoriesEnumerator = nearbyTerritoryNodeMap.Values.GetEnumerator();
            while (nearbyTerritoriesEnumerator.MoveNext()) {
                var nearbyTerritory = nearbyTerritoriesEnumerator.Current;
                if (nearbyTerritory == null) continue;
                if(CurrentLevel >= nearbyTerritory.CurrentLevel) continue;
                nearbyTerritory.CurrentLevel = CurrentLevel + 1;
                nearbyTerritory.LevelBoardCast();
            }
            nearbyTerritoriesEnumerator.Dispose();
        }

        public void PrepareForLevelBoardCast() {
            CurrentLevel = isFrontLine() ? 0 : int.MaxValue;
        }
        
        public bool isFrontLine() {
            var nearbyAreasNum = RiskGameDataModel.instance.GetNeighboursList(_areaData.Name).Count;
            var nearbyTerritoriesNum = nearbyTerritoryNodeMap.Count;
            return nearbyAreasNum != nearbyTerritoriesNum;
        }

        public void BalanceBoardCast() {
            var nearbyTerritoryNodesEnumerator = nearbyTerritoryNodeMap.Values.GetEnumerator();
            while (nearbyTerritoryNodesEnumerator.MoveNext()) {
                var nearbyTerritoriesNode = nearbyTerritoryNodesEnumerator.Current;
                if (nearbyTerritoriesNode == null) continue;
                if (CurrentLevel <= nearbyTerritoriesNode.CurrentLevel) continue;
                if (nearbyTerritoriesNode.ArmiesBalance > 0) continue;
                ArmiesBalance += nearbyTerritoriesNode.ArmiesBalance;
            }
            nearbyTerritoryNodesEnumerator.Dispose();
        }

        public void PrepareForBalanceBoardCast() {
            ArmiesBalance = _areaData.ActiveArmiesSize;
            if (!isFrontLine()) return;
            var nearbyAreasList = RiskGameDataModel.instance.GetNeighboursList(_areaData.Name);
            foreach (var nearbyAreaData in nearbyAreasList) {
                if (nearbyTerritoryNodeMap.ContainsKey(nearbyAreaData)) continue;
                ArmiesBalance -= nearbyAreaData.TotalArmiesSize;
            }
        }
    }
}