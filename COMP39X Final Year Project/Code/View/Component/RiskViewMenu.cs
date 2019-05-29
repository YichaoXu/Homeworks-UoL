using System;
using RiskSrc.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace RiskSrc.View.Component {
    
    public class RiskViewMenu: IRiskViewPanel {
        public bool isActive => core.activeSelf;
        public bool isUpToDate => core;
        
        private readonly GameObject core;
        private readonly GameObject mainMenuPanel;
        private readonly GameObject playerSettingPanel;

        public int NumberOfHuman => humanPlayerChangeComponent.NumberOfPlayer;
        public int NumberOfSimpleComputer => simpleComputerChangeComponent.NumberOfPlayer;
        public int NumberOfNormalComputer => normalComputerChangeComponent.NumberOfPlayer;
        public int NumberOfHardComputerPlayer => hardComputerChangeComponent.NumberOfPlayer;

        public int LimitationOfPlayer {
            get => RiskPlayerNumberSettingComponent.PlayerNumberLimitation;
            set => RiskPlayerNumberSettingComponent.PlayerNumberLimitation = value;
        }

        private readonly RiskPlayerNumberSettingComponent humanPlayerChangeComponent; 
        private readonly RiskPlayerNumberSettingComponent simpleComputerChangeComponent; 
        private readonly RiskPlayerNumberSettingComponent normalComputerChangeComponent; 
        private readonly RiskPlayerNumberSettingComponent hardComputerChangeComponent; 
        
        public delegate void ButtonClickDelegate();        
        public event ButtonClickDelegate FinishSettingEvent;

        public RiskViewMenu(GameObject core) {
            this.core = core;
            mainMenuPanel = core.transform.GetChild(1).gameObject;
            playerSettingPanel = core.transform.GetChild(2).gameObject;

            var allMainMenuButton = mainMenuPanel.GetComponentsInChildren<Button>();
            allMainMenuButton[0].onClick.AddListener(HandleStartGameButtonClickEvent);
            allMainMenuButton[1].onClick.AddListener(HandleContinueButtonClickEvent);
            allMainMenuButton[2].onClick.AddListener(HandleExitButtonClickEvent);

            humanPlayerChangeComponent = new RiskPlayerNumberSettingComponent(
                componentCore:playerSettingPanel.transform.GetChild(1).gameObject, 
                type: RiskPlayerType.Human
            );  
            simpleComputerChangeComponent = new RiskPlayerNumberSettingComponent(
                componentCore:playerSettingPanel.transform.GetChild(2).gameObject, 
                type: RiskPlayerType.SimpleComputer
            );
            normalComputerChangeComponent = new RiskPlayerNumberSettingComponent(
                componentCore:playerSettingPanel.transform.GetChild(3).gameObject, 
                type: RiskPlayerType.NormalComputer
            );
            hardComputerChangeComponent = new RiskPlayerNumberSettingComponent(
                componentCore:playerSettingPanel.transform.GetChild(4).gameObject, 
                type: RiskPlayerType.HardComputer
            );

            var doneButton = playerSettingPanel.transform.GetChild(5).gameObject.GetComponent<Button>();
            doneButton.onClick.AddListener(HandleDoneButtonClickEvent);
        }

        private void HandleExitButtonClickEvent() {
            Application.Quit();       
        }
        private void HandleContinueButtonClickEvent() {
            Debug.Log("CONTINUE");
        }
        private void HandleStartGameButtonClickEvent() {
            mainMenuPanel.SetActive(false);
            playerSettingPanel.SetActive(true); 
        }    
        
        private void HandleDoneButtonClickEvent() {
            FinishSettingEvent?.Invoke();
        }
        
        public void Show() {
            core.SetActive(true);
            mainMenuPanel.SetActive(true);
            playerSettingPanel.SetActive(false);
        }

        public void Hide() {
            core.SetActive(false);
        }

        private class RiskPlayerNumberSettingComponent {
            public static int PlayerNumberLimitation { get; set;  }
            private static int totalPlayerNumber;

            private readonly Text numberTextfield;
            private readonly RiskPlayerType typeOfPlayer;

            public int NumberOfPlayer {
                get {
                    var index = numberTextfield.text.IndexOf(": ", StringComparison.Ordinal) + 2;
                    var numberStr = numberTextfield.text.Substring(index) ;
                    return int.Parse(numberStr);
                }
                set => numberTextfield.text = typeOfPlayer + ": " + value;
            }

            public RiskPlayerNumberSettingComponent(GameObject componentCore, RiskPlayerType type) {
                typeOfPlayer = type;
                var allButton = componentCore.GetComponentsInChildren<Button>();
                var addButton = allButton[0];
                addButton.onClick.AddListener(HandleClickAddButton);
                var minusButton = allButton[1];
                minusButton.onClick.AddListener(HandleClickMinusButton);
                numberTextfield = componentCore.transform.GetChild(2).gameObject.GetComponent<Text>();
                NumberOfPlayer = 0;
            }

            private void HandleClickAddButton() {
                if (totalPlayerNumber >= PlayerNumberLimitation) return; 
                NumberOfPlayer += 1;
                totalPlayerNumber += 1;
            }

            private void HandleClickMinusButton() {
                if (NumberOfPlayer == 0) return; 
                totalPlayerNumber -= 1;
                NumberOfPlayer -= 1;
            }
            
        }
    }
}