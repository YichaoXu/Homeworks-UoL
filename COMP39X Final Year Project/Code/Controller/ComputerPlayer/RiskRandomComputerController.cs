using System.Collections;
using RiskSrc.Controller.Command;
using RiskSrc.Controller.Command.Multiple;
using RiskSrc.Model;
using RiskSrc.View;
using RiskSrc.View.Component;
using UnityEngine;
using Random = System.Random;

namespace RiskSrc.Controller.ComputerPlayer {
    /*The Implementation of Simple AI*/
    
    public class RiskRandomComputerController :AbstractPlayerController{
        //TODO Fix the name to simple computer
        public override event PlayerEventDelegate EndTurnEvent;
        
        //Game Model
        private RiskLogicCore riskLogicCore;
        private RiskDataManager dataManager;
        
        //Game Command Controller
        private RiskCommandInvoker commandInvoker;
        
        //The variables will be used

        public RiskRandomComputerController(string playerName):base(playerName) {}

        protected override IEnumerator StartInitiateCore() {
            // Initiate the Game Model
            riskLogicCore = RiskLogicCore.instance;
            dataManager= RiskDataManager.instance;
            commandInvoker = RiskCommandInvoker.instance;            
            var allAreasNameList = dataManager.GetAllAreasNameList();
            while (true) {
                var randomNum = new Random().Next(0, allAreasNameList.Count);
                var randomAreaName = allAreasNameList[randomNum];
                if (dataManager.GetOwnerName(randomAreaName) == null) {
                    commandInvoker.ExecuteMultipleCommand(
                        playerName, new SelectFirstCommand(playerName, randomAreaName)
                    );
                    break;
                }
            }
            yield return new WaitForSeconds(2);

            EndTurnEvent?.Invoke();
        }

        protected override IEnumerator EndTurnCore() {
            EndTurnEvent?.Invoke();
            yield break;
        }

        protected override IEnumerator StartRunCore() {
            var playingCanvas = RiskViewSearcher.instance.GetComponentByName<RiskViewPlayingCanvas>("PlayingCanvas");
            playingCanvas.Show();
            playingCanvas.HideOperationButton();
            playingCanvas.PlayerText = playerName;
            /*Random Recruit Phase*/
            var playerMaxRecruit = riskLogicCore.GetPlayerRecruitSize(playerName);
            var territoriesNameList = dataManager.GetPlayerAreaNameList(playerName);
            var randomNum = new Random().Next(0, territoriesNameList.Count);
            var randomAreaName = territoriesNameList[randomNum];
            commandInvoker.ExecuteMultipleCommand(
                playerName, new RecruitCommand(areaName: randomAreaName,recruitSize: playerMaxRecruit)
            );
            yield return new WaitForSeconds(2);
            
            /*Random Attack Phase*/
            foreach (var territoryName in territoriesNameList) {
                var localArmies = dataManager.GetAreaActiveArmiesSize(territoryName);
                if (localArmies == 0) continue;
                var neighboursNameList = dataManager.GetAreaNeighboursNameList(territoryName);
                var randomNeighbourIndex = new Random().Next(0, neighboursNameList.Count);
                var randomNeighbourName = neighboursNameList[randomNeighbourIndex];
                var neighbourAreaOwnName = dataManager.GetOwnerName(randomNeighbourName);
                if (neighbourAreaOwnName != playerName) 
                    commandInvoker.ExecuteMultipleCommand(
                        playerName, new AttackCommand(from: territoryName,to: randomNeighbourName,size: localArmies)
                    );
                else 
                    commandInvoker.ExecuteMultipleCommand(
                        playerName, new MoveCommand(from: territoryName,to: randomNeighbourName,size: localArmies)
                    );
                yield return new WaitForSeconds(2);
            }
            playingCanvas.Hide();
            EndTurnEvent?.Invoke();
        }
    }
}