using UnityEngine;
using UnityEngine.UI;

namespace RiskSrc.View.Component {
    public class RiskViewDialog: IRiskViewPanel {
        public bool isActive => core.activeSelf;
        public bool isUpToDate => core;
        
        private readonly GameObject core;
    
        private readonly Text dialogCanvasTitleTextfield;
        private readonly Text dialogCanvasContentTextfield;

        public delegate void ButtonClickDelegate();

        public event ButtonClickDelegate OnConfirmButtonClick;
        public event ButtonClickDelegate OnCancelButtonClick;

        public string Title {
            get => dialogCanvasTitleTextfield.text;
            set => dialogCanvasTitleTextfield.text = value;
        }
        
        public string Content {
            get => dialogCanvasContentTextfield.text;
            set => dialogCanvasContentTextfield.text = value;
        }


        public RiskViewDialog(GameObject core) {
            this.core = core;
            
            var allTexts = this.core.GetComponentsInChildren<Text>();
            dialogCanvasTitleTextfield = allTexts[0];
            dialogCanvasContentTextfield = allTexts[1];
            
            var allButtons = this.core.GetComponentsInChildren<Button>();
            var dialogCanvasConfirmButton = allButtons[0];
            var dialogCanvasCancelButton = allButtons[1];
            
            dialogCanvasConfirmButton.onClick.AddListener(HandleConfirmButtonClick);
            dialogCanvasCancelButton.onClick.AddListener(HandleCancelButtonClick);
        }
        public void Show() {
            core.SetActive(true);
        }

        public void Hide() {
            dialogCanvasTitleTextfield.text = "";
            dialogCanvasContentTextfield.text = "";
            core.SetActive(false);
            //TODO OPTIMISE
            OnConfirmButtonClick = null;
            OnCancelButtonClick = null;
        }
    
        private void HandleConfirmButtonClick() {
            OnConfirmButtonClick?.Invoke();
        }

        private void HandleCancelButtonClick() {
            core.SetActive(false);
            OnCancelButtonClick?.Invoke();
        }
    }
}