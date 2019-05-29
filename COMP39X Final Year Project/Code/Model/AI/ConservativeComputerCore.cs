using System;
using System.Collections.Generic;
using System.Linq;
using RiskSrc.Model.AI.Ontology;
using RiskSrc.Model.AI.Strategy;
using RiskSrc.Model.Data;
using UnityEngine;
using Random = System.Random;

namespace RiskSrc.Model.AI {
    
    public class ConservativeComputerCore: IComputerPlayerCore {
        
        private readonly PlayerData player;
        private readonly RiskGameDataModel dataModel;
        private readonly RiskComputerOntology ontology; 

        
        public ConservativeComputerCore(string playerName) {
            dataModel = RiskGameDataModel.instance;
            ontology = new RiskComputerOntology(playerName);
            player = dataModel.GetPlayer(playerName);
        }
        
        public void Observation() {
            ontology.BuildGraph();
            ontology.UpdateBalance();
        }

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
            frontLineList.ForEach(node => {
                if (node.ArmiesBalance <= 0) return;
                var areaName = node.areaData.Name;
                var nearbyEnemyTerritoriesList = dataModel.GetNeighboursList(areaName).FindAll(data => 
                    dataModel.GetOwner(data.Name) != player
                );
                
                if (node.ArmiesBalance <= nearbyEnemyTerritoriesList.Count) return;
                var activeArmiesSize = node.areaData.ActiveArmiesSize;
                nearbyEnemyTerritoriesList.ForEach(neighbourData => {
                    var attackerSize = neighbourData.TotalArmiesSize + 1;
                    var newAction = new MoveAction(
                        fromName:areaName, 
                        toName:neighbourData.Name, 
                        size:attackerSize
                    );
                    strategy.Add(newAction);
                    activeArmiesSize -= attackerSize;
                });
                var tmpIndex = 0;
                while (activeArmiesSize > 0) {
                    var tmpNode = nearbyEnemyTerritoriesList[tmpIndex % nearbyEnemyTerritoriesList.Count];
                    var newStrategy = new MoveAction(
                        fromName:areaName, 
                        toName: tmpNode.Name, 
                        size:1
                    );
                    strategy.Add(newStrategy);
                    activeArmiesSize -= 1;
                    tmpIndex += 1;
                }
            });
            return strategy;
        }

        public RiskStrategy<MoveAction> SchedulePhaseReasoning() {
            var strategy = new RiskStrategy<MoveAction>();
            ontology.HomeFront.ForEach(homeFrontNode => {
                var homeFrontAreaData = homeFrontNode.areaData;
                var activeArmiesSize = homeFrontAreaData.ActiveArmiesSize;
                if (activeArmiesSize == 0) return;
                var neighboursHighLevelNodesList = homeFrontNode.NeighbourTerritoriesNodes.FindAll(neighbourNode =>
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
                if (activeArmiesSize > 0) {
                    var randomIndex = new Random().Next(neighboursHighLevelNodesList.Count-1);
                    var newAction = new MoveAction(
                        fromName: homeFrontAreaData.Name, 
                        toName: neighboursHighLevelNodesList[randomIndex].areaData.Name,
                        size: activeArmiesSize
                    );
                    strategy.Add(newAction);
                }
            });
            return strategy;
        }
    }
    
    
}