using System.Collections;
using System.Collections.Generic;
using RiskSrc.Controller.Command;
using RiskSrc.Controller.Command.Multiple;
using RiskSrc.Model;
using RiskSrc.Model.AI;
using RiskSrc.View;
using RiskSrc.View.Component;
using UnityEngine;
using Random = System.Random;

namespace RiskSrc.Controller.ComputerPlayer {
    /*The Implementation of Simple AI*/
    
    public class RiskConservativeComputerController :AbstractPlayerController{
        
        public override event PlayerEventDelegate EndTurnEvent;
        
        //Game Model
        private RiskDataManager dataManager;
        private ConservativeComputerCore aiCore;
        
        private RiskViewPlayingCanvas playingCanvas;
        
        private Dictionary<string, int> localArmiesBalanceDictionary; 
        
        //Game Command Controller
        private RiskCommandInvoker commandInvoker;
        
        //The variables will be used

        public RiskConservativeComputerController(string playerName):base(playerName) {}

        protected override IEnumerator StartInitiateCore() {
            playingCanvas = RiskViewSearcher.instance.GetComponentByName<RiskViewPlayingCanvas>("PlayingCanvas");
            // Initiate the Game Model
            dataManager= RiskDataManager.instance;
            commandInvoker = RiskCommandInvoker.instance;
            aiCore = new ConservativeComputerCore(playerName);
            var areaName =
                SelectFirstTerritoryNameIfNobodyOccupiesAnyAreaInContinent("Africa")
                ?? SelectFirstTerritoryNameIfNobodyOccupiesAnyAreaInContinent("Oceania")
                ?? SelectFirstTerritoryNameIfNobodyOccupiesAnyAreaInContinent("South America")
                ?? RandomlySelectFirstTerritoryNameFromAllAreas();
            var command = new SelectFirstCommand(playerName, areaName);
            commandInvoker.ExecuteMultipleCommand(playerName, command);
            yield return new WaitForSeconds(2);
            EndTurnEvent?.Invoke();
        }

        private string SelectFirstTerritoryNameIfNobodyOccupiesAnyAreaInContinent (string continentName) {
            var continentAreasNameList = dataManager.GetContinentAreasNameList(continentName);
            var isContinentOccupied = ! continentAreasNameList.TrueForAll(oceaniaAreaName => 
                dataManager.GetOwnerName(oceaniaAreaName) == null
            );
            if (isContinentOccupied) return null;
            return RandomlySelectFirstTerritoryName(fromNameList:continentAreasNameList);
        }

        private string RandomlySelectFirstTerritoryNameFromAllAreas() {
            var allAreaNameList = dataManager.GetAllAreasNameList();
            return RandomlySelectFirstTerritoryName(fromNameList:allAreaNameList);
        }

        private string RandomlySelectFirstTerritoryName(List<string> fromNameList) {
            while (true) {
                var randomNum = new Random().Next(0, fromNameList.Count);
                var randomAreaName = fromNameList[randomNum];
                if (dataManager.GetOwnerName(randomAreaName) == null) return randomAreaName;
            }
        }

        protected override IEnumerator StartRunCore() {
            playingCanvas.Show();
            playingCanvas.HideOperationButton();
            playingCanvas.PlayerText = playerName;
            /*Recruit Phase*/
            playingCanvas.PhaseText = "RECRUIT";
            yield return Reinforcement();
            /*Attack Phase*/
            playingCanvas.PhaseText = "ATTACK";
            yield return Attack();
            /*Schedule Phase*/
            playingCanvas.PhaseText = "SCHEDULE";
            yield return Schedule();
            
            playingCanvas.Hide();
            RiskLogicCore.instance.EndPlayerTurn(playerName);
            EndTurnEvent?.Invoke();
        }

        private IEnumerator Reinforcement() {
            aiCore.Observation();
            var recruitStrategy = aiCore.DeployPhaseReasoning();
            var recruitActionEnumerator = recruitStrategy.GetEnumerator();
            while(recruitActionEnumerator.MoveNext()) {
                var recruitAction = recruitActionEnumerator.Current;
                if (recruitAction == null) continue;
                var command = new RecruitCommand(recruitAction.targetAreaName, recruitAction.ArmiesSize);
                commandInvoker.ExecuteMultipleCommand(playerName, command);
                yield return new WaitForSeconds(2);
            }
            recruitActionEnumerator.Dispose();
        }

        private IEnumerator Attack() {
            aiCore.Observation();
            var attackStrategy = aiCore.AttackPhaseReasoning();
            var attackActionEnumerator = attackStrategy.GetEnumerator();
            while(attackActionEnumerator.MoveNext()) {
                var attackAction = attackActionEnumerator.Current;
                if (attackAction == null) continue;
                if(dataManager.GetOwnerName(attackAction.ToAreaName) == playerName) continue;
                var command = new AttackCommand( 
                    from: attackAction.FromAreaName,
                    to: attackAction.ToAreaName,
                    size: attackAction.ArmiesSize
                );
                commandInvoker.ExecuteMultipleCommand(playerName, command);
                yield return new WaitForSeconds(2);
            }
            attackActionEnumerator.Dispose();
        }

        private IEnumerator Schedule() {
            aiCore.Observation();
            var scheduleStrategy = aiCore.SchedulePhaseReasoning();
            var scheduleActionEnumerator = scheduleStrategy.GetEnumerator();
            while(scheduleActionEnumerator.MoveNext()) {
                var scheduleAction = scheduleActionEnumerator.Current;
                if (scheduleAction == null) continue;
                var command = new MoveCommand( 
                    from: scheduleAction.FromAreaName,
                    to: scheduleAction.ToAreaName,
                    size: scheduleAction.ArmiesSize
                );
                commandInvoker.ExecuteMultipleCommand(playerName, command);
                yield return new WaitForSeconds(2);
            }
            scheduleActionEnumerator.Dispose();
        }
        
        protected override IEnumerator EndTurnCore() {
            EndTurnEvent?.Invoke();
            yield break;
        }
    }
    

}