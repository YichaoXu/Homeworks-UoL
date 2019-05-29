using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RiskSrc.Controller.Command;
using RiskSrc.Controller.Command.Unique;
using RiskSrc.Model;
using RiskSrc.Model.IO;
using RiskSrc.View;
using RiskSrc.View.Component;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RiskSrc.Controller {
    public class RiskControllerManager : MonoBehaviour {

        private RiskViewMenu startMenu;
        private RiskLoadingPanel loadingPanel;
        private RiskPlayerIdentifyPanel playerIdentifyPanel;
        private RiskInitiator riskInitiator;

        private List<AbstractPlayerController> playerControllers;

        private int totalRunTimes;
        private int currentPlayerIndex => totalRunTimes % playerControllers.Count;
        private int currentTurn => totalRunTimes / playerControllers.Count;

        private void Awake() {
            riskInitiator = new RiskInitiator();
            playerControllers = new List<AbstractPlayerController>();
        }

        private void Start() {
            var viewSearcher = RiskViewSearcher.instance;
            startMenu = viewSearcher.GetComponentByName<RiskViewMenu>("StartMenu");
            loadingPanel = viewSearcher.GetComponentByName<RiskLoadingPanel>("LoadingPanel");
            playerIdentifyPanel = viewSearcher.GetComponentByName<RiskPlayerIdentifyPanel>("PlayerIdentifyPanel");
        
            startMenu.Show();
            startMenu.FinishSettingEvent += GameStart;
            startMenu.LimitationOfPlayer = JsonFile.GetJsonFile(FilePath.RuleSettingAbsPath).Read()["MaxPlayerSize"];
        }

        private void Update() {
            if (playerControllers.Count == 0 || currentTurn < 1) return;
            var isGameOver =
                playerControllers.Count(controller => controller.isGameOver()) == (playerControllers.Count - 1);
            if (isGameOver) GameOver();
        }

        private void GameStart() {
            IncreasePlayerController(RiskPlayerType.Human, startMenu.NumberOfHuman);
            IncreasePlayerController(RiskPlayerType.SimpleComputer, startMenu.NumberOfSimpleComputer);
            IncreasePlayerController(RiskPlayerType.NormalComputer, startMenu.NumberOfNormalComputer);
            IncreasePlayerController(RiskPlayerType.HardComputer, startMenu.NumberOfHardComputerPlayer);
            riskInitiator.Finish();
            startMenu.Hide();
            loadingPanel.Show();
            playerIdentifyPanel.Show();
            StartCoroutine(InitiateCoroutine());

            IEnumerator InitiateCoroutine() {
                yield return null;
                RiskCommandInvoker.instance.ExecuteExclusiveCommand(new ContinentInitiateCommand());
                RiskCommandInvoker.instance.ExecuteExclusiveCommand(new ArmiesInitiateCommand());
                StartCoroutine(playerControllers.First().StartRun());
                loadingPanel.Hide();
            }
        }

        private void GameOver() {
            var winnerName = playerControllers.Find(controller => !controller.isGameOver()).playerName;
            var dialog = RiskViewSearcher.instance.GetComponentByName<RiskViewDialog>("DialogPanel");
            if (dialog.isActive) return;
            dialog.Title = "Game Over";
            dialog.Content = "Winner is " + winnerName;
            dialog.OnConfirmButtonClick += () => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            dialog.OnCancelButtonClick += Application.Quit;
            dialog.Show();
        }

        private void IncreasePlayerController(RiskPlayerType type, int number) {
            var builder = new PlayerControllerBuilder();
            builder.PlayerType(type);
            while (number != 0) {
                var playerName = type.ToString().Substring(0, 1) + playerControllers.Count;
                riskInitiator.CreateGamePlayer(playerName, type);
                builder.PlayerName(playerName);
                var playerController = builder.Build();
                playerController.EndTurnEvent += MoveToNextPlayer;
                playerControllers.Add(playerController);
                number -= 1;
            }
        }

        private void MoveToNextPlayer() {
            totalRunTimes += 1;
            var viewSearcher = RiskViewSearcher.instance;
            var playerCanvas = viewSearcher.GetComponentByName<RiskViewPlayingCanvas>("PlayingCanvas");
            playerCanvas.TurnNumber = currentTurn;
            playerIdentifyPanel.UpdateAllPlayers();
            StartCoroutine(
                playerControllers[currentPlayerIndex].StartRun()
            );
        }
    }
}
    
