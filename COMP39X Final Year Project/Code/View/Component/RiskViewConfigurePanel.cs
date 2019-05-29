using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace RiskSrc.View.Component {

    public class RiskViewConfigurePanel: IRiskViewPanel{
        public bool isActive => core.activeSelf;
        public bool isUpToDate => core;

        private readonly GameObject core;
        private readonly Text configureCanvasTitleTextfield;
        private readonly Text limitationOfConfigTextfield;
        private readonly Text configTextfield;
        private readonly Slider configureSlider;

        public delegate void ButtonClickDelegate();
        public delegate void SliderChangeDelegate(int number);
        public event ButtonClickDelegate ConfirmButtonClick;
        public event ButtonClickDelegate CancelButtonClick;
        public event SliderChangeDelegate SliderChange;

        public string Title {
            get => configureCanvasTitleTextfield.text;
            set => configureCanvasTitleTextfield.text = value;
        }

        public int Limitation{
            get {
                var intStr = limitationOfConfigTextfield.text.Substring(startIndex: 6);
                return int.Parse(intStr);
            }
            set => limitationOfConfigTextfield.text = "Left: " + value;
        }

        public RiskViewConfigurePanel(GameObject core) {
            this.core = core;
            
            configureSlider = core.GetComponentInChildren<Slider>();

            var allTextfield = core.GetComponentsInChildren<Text>();
            configureCanvasTitleTextfield = allTextfield[0];
            limitationOfConfigTextfield = allTextfield[1];
            configTextfield = allTextfield[2];
                  
            var configureConfirmButton = core.GetComponentsInChildren<Button>()[0];
            configureConfirmButton.onClick.AddListener(HandleConfirmButtonClick);
            configureSlider.onValueChanged.AddListener(HandleScrollValueChange);
            var configureCancelButton = core.GetComponentsInChildren<Button>()[1];
            configureCancelButton.onClick.AddListener(HandleCancelButtonClick); 
        }
        public void Show() {
            core.SetActive(true);
        }

        public void Hide() {
            configureSlider.value = 0;
            limitationOfConfigTextfield.text = "0";
            configureCanvasTitleTextfield.text = "";
            core.SetActive(false);
        }

        private void HandleConfirmButtonClick() {
            ConfirmButtonClick?.Invoke();
        }
        
        private void HandleCancelButtonClick(){
            CancelButtonClick?.Invoke();
        }

        private void HandleScrollValueChange(float rate) {
            var currentConfig = (int) Math.Floor(Limitation * rate);
            configTextfield.text = "Number:" + currentConfig;
            SliderChange?.Invoke(currentConfig);
        }
    }
}