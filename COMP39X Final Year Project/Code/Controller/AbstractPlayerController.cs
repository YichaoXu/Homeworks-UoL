using System;
using System.Collections;
using RiskSrc.Controller.ComputerPlayer;
using RiskSrc.Controller.HumanPlayer;
using RiskSrc.Model;
using UnityEngine;

namespace RiskSrc.Controller {
    public abstract class AbstractPlayerController {

        private bool isInitiated;
        
        public readonly string playerName;

        public delegate void PlayerEventDelegate();
        public abstract event PlayerEventDelegate EndTurnEvent;

        protected AbstractPlayerController(string name) {
            isInitiated = false;
            playerName = name;
        }
        
        public IEnumerator StartRun() {
            if (!isInitiated) { // if the game player does not finish initiation
                isInitiated = true;
                yield return StartInitiateCore();
            }
            else if (isGameOver()) {
                yield return EndTurnCore();
            }
            else {
                yield return StartRunCore();
            }
        }

        public bool isGameOver() {
            return RiskLogicCore.instance.isPlayerGameOver(playerName);
        }

        protected abstract IEnumerator StartRunCore();
        protected abstract IEnumerator StartInitiateCore();
        protected abstract IEnumerator EndTurnCore();
        
    }

    public class PlayerControllerBuilder {
        private RiskPlayerType playerType;
        private string playerName;

        public PlayerControllerBuilder PlayerName(string name) {
            playerName = name;
            return this;
        }

        public PlayerControllerBuilder PlayerType(RiskPlayerType type) {
            playerType = type;
            return this;
        }

        public AbstractPlayerController Build() {
            switch (playerType) {
                case RiskPlayerType.Human:
                    return new RiskHumanPlayerController(playerName);
                case RiskPlayerType.SimpleComputer: 
                    return new RiskRandomComputerController(playerName);
                case RiskPlayerType.NormalComputer:
                    return new RiskConservativeComputerController(playerName);
                case RiskPlayerType.HardComputer:
                    return new RiskAggressiveComputerController(playerName);
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }
        
    }
}