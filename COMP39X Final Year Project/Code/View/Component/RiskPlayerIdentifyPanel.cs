using System.Collections.Generic;
using System.Runtime.InteropServices;
using RiskSrc.Model;
using RiskSrc.Model.Data;
using RiskSrc.Model.IO;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

namespace RiskSrc.View.Component {
    public class RiskPlayerIdentifyPanel:IRiskViewPanel {
        public bool isActive => core.activeSelf;
        public bool isUpToDate => core;

        
        private readonly GameObject core;
        private readonly GameObject contentArea;
        private readonly GameObject contentItemSample;
        private readonly Button displayButton;

        private readonly Dictionary<string, GameObject> playerDataGameObjectBackup;
        
        public RiskPlayerIdentifyPanel(GameObject panelCore) {
            playerDataGameObjectBackup = new Dictionary<string, GameObject>();
            core = panelCore;
            displayButton = panelCore.GetComponentInChildren<Button>();
            displayButton.onClick.AddListener(DisplayAllEvent);
            contentArea = panelCore.transform.Find("Event/Viewport/Content").gameObject;
            contentItemSample = contentArea.transform.GetChild(0).gameObject;
        }

        public void UpdateAllPlayers() {
            RiskGameDataModel.instance.GetAllPlayersData().ForEach(playerData => Update(playerData.Name));
        }

        private void Update(string playerName) {
            if (!playerDataGameObjectBackup.ContainsKey(playerName)) {
                var newItem = Object.Instantiate(contentItemSample, contentArea.transform);
                var itemTransform = contentItemSample.GetComponent<RectTransform>();
                var itemPosition = itemTransform.localPosition;
                var contentAreaTransform = contentArea.GetComponent<RectTransform>();
                var itemSize = itemTransform.sizeDelta;
                var numberOfEvent = playerDataGameObjectBackup.Count;
                contentAreaTransform.sizeDelta = new Vector2(
                    x: contentAreaTransform.sizeDelta.x,
                    y: itemSize.y * (numberOfEvent + 1));
                newItem.transform.localPosition = new Vector3(
                    x: itemPosition.x,
                    y: -itemSize.y * numberOfEvent,
                    z: itemPosition.z
                );
                var colorSetting = JsonFile.GetJsonFile(FilePath.DecorationSettingAbsPath).Read();
                newItem.GetComponentInChildren<Image>().color = colorSetting["Players"][playerName].AsColor().Value;
                newItem.SetActive(true);
                playerDataGameObjectBackup[playerName] = newItem;
            }
            var playerDataGameObject = playerDataGameObjectBackup[playerName];
            playerDataGameObject.GetComponentInChildren<Text>().text =
                "PlayerName: " + playerName + ";\n" +
                "Recruit: " + RiskLogicCore.instance.GetPlayerRecruitSize(playerName) + " per turn";
        }


        public void Show() {
            core.SetActive(true);
            UpdateAllPlayers();
        }

        public void Hide() {
            core.SetActive(false);
        }

        private void DisplayAllEvent() {
            displayButton.onClick.RemoveListener(DisplayAllEvent);
            displayButton.onClick.AddListener(CancelDisplay);
            var panelGameObject = core.transform.GetChild(0).gameObject;
            var panelWidth = panelGameObject.GetComponent<RectTransform>().sizeDelta.x;
            HorizontallyMove(panelGameObject, panelWidth);
            var buttonGameObject  = displayButton.gameObject;
            var buttonWidth = buttonGameObject.GetComponent<RectTransform>().sizeDelta.x;
            HorizontallyMove(buttonGameObject, -buttonWidth);
            buttonGameObject.GetComponentInChildren<Text>().text = "<";
        }

        private void CancelDisplay() {
            displayButton.onClick.RemoveListener(CancelDisplay);
            displayButton.onClick.AddListener(DisplayAllEvent);
            var panelGameObject = core.transform.GetChild(0).gameObject;
            var panelWidth = panelGameObject.GetComponent<RectTransform>().sizeDelta.x;
            HorizontallyMove(panelGameObject, -panelWidth);
            var buttonGameObject  = displayButton.gameObject;
            var buttonWidth = buttonGameObject.GetComponent<RectTransform>().sizeDelta.x;
            HorizontallyMove(buttonGameObject, buttonWidth);
            buttonGameObject.GetComponentInChildren<Text>().text = ">";
        }

        private void HorizontallyMove(GameObject gameObject, float value) {
            var previousPosition = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(
                x: previousPosition.x + value, 
                y: previousPosition.y,
                z: previousPosition.z
            );
        }

    }
}