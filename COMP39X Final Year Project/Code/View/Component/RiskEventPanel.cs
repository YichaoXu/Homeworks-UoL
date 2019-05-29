using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RiskSrc.View.Component {
    public class RiskEventPanel:IRiskViewPanel {
        public bool isActive => core.activeSelf;
        public bool isUpToDate => core;

        
        private readonly GameObject core;
        private readonly GameObject contentArea;
        private readonly GameObject textFieldContainerSample;
        private readonly Button displayButton;

        private readonly List<string> eventBackup;
        
        public RiskEventPanel(GameObject panelCore) {
            eventBackup = new List<string>();
            
            core = panelCore;
            displayButton = core.GetComponentInChildren<Button>();
            displayButton.onClick.AddListener(DisplayAllEvent);
            contentArea = core.transform.Find("Event/Viewport/Content").gameObject;
            textFieldContainerSample = contentArea.transform.GetChild(0).gameObject;
        }

        public void AddEventDescription(string description) {
            var tmpTextfield = Object.Instantiate(textFieldContainerSample, contentArea.transform);
            var textfieldTransform = textFieldContainerSample.GetComponent<RectTransform>();
            var textfieldPosition = textfieldTransform.localPosition;
            var contentAreaTransform = contentArea.GetComponent<RectTransform>();
            var textfieldSize = textfieldTransform.sizeDelta;
            var numberOfEvent = eventBackup.Count;
            contentAreaTransform.sizeDelta = new Vector2(
                x: contentAreaTransform.sizeDelta.x, 
                y: textfieldSize.y * (numberOfEvent+1));
            tmpTextfield.transform.localPosition = new Vector3(
                x: textfieldPosition.x, 
                y: -textfieldSize.y * numberOfEvent, 
                z: textfieldPosition.z
            );
            tmpTextfield.GetComponent<Text>().text = description;
            eventBackup.Add(description);
            Debug.Log(description);
        }

        
        public void Show() {
            core.SetActive(true);
        }

        public void Hide() {
            core.SetActive(false);
        }

        private void DisplayAllEvent() {
            displayButton.onClick.RemoveListener(DisplayAllEvent);
            displayButton.onClick.AddListener(CancelDisplay);
            var panel = core.transform.GetChild(0).gameObject;
            var height = panel.GetComponent<RectTransform>().sizeDelta.y;
            MoveDisplayPanelVertically(height);
        }

        private void CancelDisplay() {
            displayButton.onClick.RemoveListener(CancelDisplay);
            displayButton.onClick.AddListener(DisplayAllEvent);
            var panel = core.transform.GetChild(0).gameObject;
            var height = panel.GetComponent<RectTransform>().sizeDelta.y;
            MoveDisplayPanelVertically(-height);
        }

        private void MoveDisplayPanelVertically(float value) {
            var panel = core.transform.GetChild(0).gameObject;
            var previousPosition = panel.transform.localPosition;
            panel.transform.localPosition = new Vector3(
                x: previousPosition.x, 
                y: previousPosition.y + value,
                z: previousPosition.z
            );
        }

    }
}