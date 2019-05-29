using UnityEngine;
using UnityEngine.UI;

namespace RiskSrc.View.Component {
    public class RiskLoadingPanel :IRiskViewPanel {
        public bool isActive => core.activeSelf;
        public bool isUpToDate => core;

        private readonly GameObject core;   
        private readonly Text loadingTextfield;
        
        public RiskLoadingPanel(GameObject panelCore) {
            core = panelCore;
        }
        public void Show() {
            core.SetActive(true);
        }

        public void Hide() {
            core.SetActive(false);
        }
    }
}