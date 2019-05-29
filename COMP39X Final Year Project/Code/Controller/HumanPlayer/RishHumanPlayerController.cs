using System.Collections;
using System.Collections.Generic;
using RiskSrc.Controller.HumanPlayer.Map;
using RiskSrc.Controller.HumanPlayer.Phase;
using RiskSrc.View;
using RiskSrc.View.Component;
using UnityEngine;

namespace RiskSrc.Controller.HumanPlayer {
    public class RiskHumanPlayerController: AbstractPlayerController  {
        private enum GamePhase {INITIATE, START, RECRUIT, ATTACK, END}
        
        public override event PlayerEventDelegate EndTurnEvent;

        private readonly RiskViewPlayingCanvas gameCanvas;
        private readonly Dictionary<GamePhase, AbstractHumanPlayerPhaseControllerCore> controllerDictionary;

        private AbstractHumanPlayerPhaseControllerCore _currentPhaseControllerCore;
        

        public RiskHumanPlayerController(string playerName) : base(playerName) {
            controllerDictionary = new Dictionary<GamePhase, AbstractHumanPlayerPhaseControllerCore>();
            gameCanvas = RiskViewSearcher.instance.GetComponentByName<RiskViewPlayingCanvas>("PlayingCanvas");
        }
        
        protected override IEnumerator StartInitiateCore() {
            var dialog = RiskViewSearcher.instance.GetComponentByName<RiskViewDialog>("DialogPanel");
            var configPanel = RiskViewSearcher.instance.GetComponentByName<RiskViewConfigurePanel>("ConfigurePanel");
            
            var tmpController = new InitiatePhaseControllerCore(playerName, dialog);
            tmpController.InitiateFinishEvent += EndInitiate;
            controllerDictionary[GamePhase.INITIATE] =  tmpController;
            
            controllerDictionary[GamePhase.START] 
                = new TurnStartPhaseControllerCore(playerName);
            
            controllerDictionary[GamePhase.RECRUIT] 
                = new RecruitPhaseControllerCore(playerName, configPanel);
            
            controllerDictionary[GamePhase.ATTACK] 
                = new AttackPhaseControllerCore(playerName, configPanel);
            
            controllerDictionary[GamePhase.END] 
                = new TurnEndPhaseControllerCore(playerName:playerName);
            
            SetCurrentController(GamePhase.INITIATE);
            yield break;
        }

        private void EndInitiate() {
            SetCurrentController(GamePhase.END);
            EndTurnEvent?.Invoke();
        }

        protected override IEnumerator StartRunCore() {
            gameCanvas.Show();
            gameCanvas.PlayerText = playerName;
            gameCanvas.OnClickNextTurnButton += EndTurn;
            gameCanvas.OnClickRecruitTurnButton += StartRecruitPhase;
            gameCanvas.OnClickAttackButton += StartAttackPhase;
            gameCanvas.OnClickExitButton += Application.Quit;
            SetCurrentController(GamePhase.START);
            yield break;
        }
        
        private void EndTurn() {
            gameCanvas.Hide();
            gameCanvas.OnClickNextTurnButton -= EndTurn;
            gameCanvas.OnClickRecruitTurnButton -= StartRecruitPhase;
            gameCanvas.OnClickAttackButton -= StartAttackPhase;
            gameCanvas.OnClickExitButton -= Application.Quit;
            SetCurrentController(GamePhase.END);
            EndTurnEvent?.Invoke();
        }
        
        protected override IEnumerator EndTurnCore() {
            EndTurnEvent?.Invoke();
            yield break;
        }


        private void StartAttackPhase() {
            SetCurrentController(GamePhase.ATTACK);
        }

        private void StartRecruitPhase() {
            SetCurrentController(GamePhase.RECRUIT);
        }

        private void SetCurrentController(GamePhase atPhase) {
            _currentPhaseControllerCore?.Stop();
            _currentPhaseControllerCore = controllerDictionary[atPhase];
            _currentPhaseControllerCore.Work();
            gameCanvas.PhaseText = atPhase.ToString();
        }
    }
}