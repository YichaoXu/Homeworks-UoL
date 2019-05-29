using UnityEngine;
using UnityEngine.UI;

namespace RiskSrc.View.Component {
    public class RiskViewPlayingCanvas:IRiskViewPanel {
        public bool isActive => core.activeSelf;
        public bool isUpToDate => core;
        
        private readonly GameObject core;

        private readonly Text turnTextfield;
        private readonly Text playerTextfield;
        private readonly Text phaseTextfield;
        private readonly Button nextTurnButton;
        private readonly Button attackTurnButton;
        private readonly Button recruitTurnButton;
        private readonly Button exitButtonTurnButton;

        public int TurnNumber {
            get {
                var intStr = turnTextfield.text.Substring(startIndex: 5);
                return int.Parse(intStr);
            }
            set => turnTextfield.text = "TURN " + value;
        }
        public string PhaseText {
            get => phaseTextfield.text;
            set => phaseTextfield.text = value;
        }

        public string PlayerText {
            get => playerTextfield.text;
            set => playerTextfield.text = value;
        }

        public delegate void ButtonClickEvent();

        public event ButtonClickEvent OnClickNextTurnButton;
        public event ButtonClickEvent OnClickAttackButton;
        public event ButtonClickEvent OnClickRecruitTurnButton;
        public event ButtonClickEvent OnClickExitButton;




        public RiskViewPlayingCanvas(GameObject core) {
            this.core = core;
            var topArea = this.core.GetComponentsInChildren<Image>()[0];
            
            var topAreaAllTextfield = topArea.GetComponentsInChildren<Text>();
            turnTextfield = topAreaAllTextfield[0];
            playerTextfield = topAreaAllTextfield[1];
            phaseTextfield = topAreaAllTextfield[2];
            
            var topAreaAllButton = topArea.GetComponentsInChildren<Button>();
            nextTurnButton = topAreaAllButton[0];
            attackTurnButton = topAreaAllButton[1];
            recruitTurnButton = topAreaAllButton[2];
            exitButtonTurnButton = topAreaAllButton[3];
            
            nextTurnButton.onClick.AddListener(GenerateNextTurnButtonClickEvent);
            recruitTurnButton.onClick.AddListener(GenerateRecruitButtonClickEvent);
            attackTurnButton.onClick.AddListener(GenerateAttackButtonClickEvent);
        }
        
        public void Show() {
            core.SetActive(true);
            nextTurnButton.gameObject.SetActive(true);
            exitButtonTurnButton.gameObject.SetActive(true);
            recruitTurnButton.gameObject.SetActive(true);
            attackTurnButton.gameObject.SetActive(false);
        }

        public void Hide() {
            core.SetActive(false);
        }

        public void HideOperationButton() {
            nextTurnButton.gameObject.SetActive(false);
            recruitTurnButton.gameObject.SetActive(false);
            attackTurnButton.gameObject.SetActive(false);
            exitButtonTurnButton.gameObject.SetActive(false);
        }
        
        private void GenerateNextTurnButtonClickEvent() {
            OnClickNextTurnButton?.Invoke();
            nextTurnButton.gameObject.SetActive(true);
            recruitTurnButton.gameObject.SetActive(true);
            attackTurnButton.gameObject.SetActive(false);
        }
        
        private void GenerateRecruitButtonClickEvent() {
            OnClickRecruitTurnButton?.Invoke();
            recruitTurnButton.gameObject.SetActive(false);
            attackTurnButton.gameObject.SetActive(true);
        }

        private void GenerateAttackButtonClickEvent() {
            OnClickAttackButton?.Invoke();
            attackTurnButton.gameObject.SetActive(false);
        }
        
        private void GenerateExitButtonClickEvent() {
            OnClickExitButton?.Invoke();
        }
    }
}