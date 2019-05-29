using System;
using System.Collections.Generic;
using System.Linq;
using RiskSrc.Model.AI.Ontology;
using RiskSrc.Model.AI.Strategy;
using RiskSrc.Model.Data;
using UnityEngine;

namespace RiskSrc.Model.AI {
    public class AggressiveComputerCore: IComputerPlayerCore {
        private readonly PlayerData player;
        private readonly RiskGameDataModel dataModel;
        private readonly RiskComputerOntology ontology; 

        
        public AggressiveComputerCore(string playerName) {
            dataModel = RiskGameDataModel.instance;
            ontology = new RiskComputerOntology(playerName);
            player = dataModel.GetPlayer(playerName);
        }

        public void Observation() {
            ontology.BuildGraph();
            ontology.UpdateBalance();
        }
        
        /*
         * If the size of recruit is smaller than the sum of negative balance for all frontline nodes,
         * deploys recruits into each of these nodes as many as possible and the size of recruits
         * equals to its balance value.
         * If the size of recruit is lager, the AIcore will increase army’s size of these nodes to
         * make their balance be zero and then averagely deploys remaining recruits.
         */
        public RiskStrategy<ReinforcementAction> DeployPhaseReasoning() {
            var strategy = new RiskStrategy<ReinforcementAction>();
            var recruitNumber = RiskLogicCore.instance.GetPlayerRecruitSize(player.Name);
            var frontLineList = ontology.FrontLine;
            frontLineList.ForEach(node => {
                if (recruitNumber <= 0) return;
                if (node.ArmiesBalance >= 0) return;
                var armiesShortage = -node.ArmiesBalance;
                var newAction = new ReinforcementAction(
                    areaName:node.areaData.Name, size:Math.Min(armiesShortage, recruitNumber)
                );
                strategy.Add(newAction);
                recruitNumber -= armiesShortage;
            });
            if (recruitNumber <= 0) return strategy;
            var tmpIndex = 0;
            while (recruitNumber > 0) {
                var tmpNode = frontLineList[tmpIndex % frontLineList.Count];
                var newStrategy = new ReinforcementAction(
                    areaName: tmpNode.areaData.Name, size: 1
                );
                strategy.Add(newStrategy);
                recruitNumber -= 1;
                tmpIndex += 1;
            }
            return strategy;
        }

        public RiskStrategy<MoveAction> AttackPhaseReasoning() {
            var strategy = new RiskStrategy<MoveAction>();
            var frontLineList = ontology.FrontLine;
            frontLineList.ForEach(frontLineNode => {
                var actionList = frontLineNode.ArmiesBalance >= 0
                    ? AttackWithAbsoluteNumericalSuperiority(frontLineNode)
                    : AttackWithRelativeNumericalSuperiority(frontLineNode);
                actionList.ForEach(action => strategy.Add(action));
            });
            return strategy;
        }
        
        /*
         * If the player has absolute numerical superiority in current area, comparing with other
         * players who occupy adjacent areas. Therefore, the player core will attack these adjacent
         * areas which are under enemies’ control. 
         */
        private List<MoveAction> AttackWithAbsoluteNumericalSuperiority(BalanceNode frontLineNode){
            /*attack adjacent areas which are under enemies’ control*/
            var actionList = new List<MoveAction>();
            var frontLineAreaData = frontLineNode.areaData;
            var nearbyAreasList = dataModel.GetNeighboursList(frontLineAreaData.Name);
            var nearbyEnemyAreasList = nearbyAreasList.FindAll(data =>
                dataModel.GetOwner(data.Name) != player
            );
            var availableAttackerSize = frontLineAreaData.ActiveArmiesSize;
            nearbyEnemyAreasList.ForEach(neighbourData => {
                var attackerSize = neighbourData.TotalArmiesSize;
                var newAction = new MoveAction(
                    fromName:frontLineAreaData.Name, 
                    toName:neighbourData.Name, 
                    size:attackerSize
                );
                actionList.Add(newAction);
                availableAttackerSize -= attackerSize;
            });
            /*Averagely increase the size of attacker*/
            var tmpIndex = 0;
            while (availableAttackerSize > 0) {
                tmpIndex = tmpIndex % nearbyEnemyAreasList.Count;
                var enemyAreaData = nearbyEnemyAreasList[tmpIndex];
                var newStrategy = new MoveAction(
                    fromName:frontLineAreaData.Name, 
                    toName: enemyAreaData.Name, 
                    size:1
                );
                actionList.Add(newStrategy);
                availableAttackerSize -= 1;
                tmpIndex += 1;
            }

            return actionList;
        }
        /*
         * if the player has relative numerical superiority in the area, the player core will attack
         * these adjacent enemy-territories until it loses the superiority. 
         */
        private List<MoveAction> AttackWithRelativeNumericalSuperiority(BalanceNode frontLineNode) {
            var actionList = new List<MoveAction>();
            var frontLineAreaData = frontLineNode.areaData;
            var nearbyAreasList = dataModel.GetNeighboursList(frontLineAreaData.Name);
            var nearbyAreasListInEnemyTerritories = nearbyAreasList.FindAll(data =>
                dataModel.GetOwner(data.Name) != player
            );
            var availableAttackerSize = frontLineAreaData.ActiveArmiesSize;
            var nearbyEnemyTerritoryMaxArmiesSize = 
                nearbyAreasListInEnemyTerritories.Max(enemyTerritory=>enemyTerritory.TotalArmiesSize);
            if(nearbyEnemyTerritoryMaxArmiesSize > availableAttackerSize) return actionList;
            availableAttackerSize -= nearbyEnemyTerritoryMaxArmiesSize;
            nearbyAreasListInEnemyTerritories.ForEach(nearbyEnemyTerritoryData => {
                if (availableAttackerSize < nearbyEnemyTerritoryData.TotalArmiesSize) return;
                var attackerSize = nearbyEnemyTerritoryData.TotalArmiesSize;
                var newAction = new MoveAction(
                    fromName:frontLineAreaData.Name, 
                    toName:nearbyEnemyTerritoryData.Name, 
                    size:attackerSize
                );
                actionList.Add(newAction);
                availableAttackerSize -= attackerSize;
            });
            var tmpIndex = 0;
            while (availableAttackerSize > 0) {
                if(actionList.Count ==0) break;
                tmpIndex = tmpIndex % actionList.Count;
                var eachAction = actionList[tmpIndex];
                if(eachAction == null) continue;
                var newAction = new MoveAction(
                    fromName: eachAction.FromAreaName, 
                    toName: eachAction.ToAreaName,
                    size: 1
                );
                actionList.Add(newAction);
                availableAttackerSize -= 1;
            }
            return actionList;
        }

        public RiskStrategy<MoveAction> SchedulePhaseReasoning() {
            var strategy = new RiskStrategy<MoveAction>();
            ontology.HomeFront.ForEach(homeFrontNode => {
                var homeFrontAreaData = homeFrontNode.areaData;
                var activeArmiesSize = homeFrontAreaData.ActiveArmiesSize;
                if (activeArmiesSize == 0) return;
                var nodeNeighbours = homeFrontNode.NeighbourTerritoriesNodes;
                var neighboursHighLevelNodesList = nodeNeighbours.FindAll(neighbourNode =>
                    neighbourNode.CurrentLevel < homeFrontNode.CurrentLevel
                );
                neighboursHighLevelNodesList.ForEach(neighbourNode => {
                    if (activeArmiesSize <= 0) return;
                    if (neighbourNode.ArmiesBalance >= 0) return;
                    var armiesShortage = -neighbourNode.ArmiesBalance;
                    var newAction = new MoveAction(
                        fromName: homeFrontAreaData.Name,
                        toName: neighbourNode.areaData.Name, 
                        size: Math.Min(activeArmiesSize, armiesShortage)
                    );
                    strategy.Add(newAction);
                    activeArmiesSize -= armiesShortage;
                });
                var tmpIndex = 0;
                while (activeArmiesSize > 0) {
                    tmpIndex = tmpIndex % neighboursHighLevelNodesList.Count;
                    var highLevelNodes = neighboursHighLevelNodesList[tmpIndex];
                    var newStrategy = new MoveAction(
                        fromName:homeFrontAreaData.Name, 
                        toName: highLevelNodes.areaData.Name, 
                        size:1
                    );
                    strategy.Add(newStrategy);
                    activeArmiesSize -= 1;
                    tmpIndex += 1;
                }
            });
            return strategy;
        }
    }
}