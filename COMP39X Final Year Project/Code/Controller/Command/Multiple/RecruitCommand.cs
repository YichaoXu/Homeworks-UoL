using RiskSrc.Model;
using RiskSrc.View.Map;
using UnityEngine;

namespace RiskSrc.Controller.Command.Multiple {


    public class RecruitCommand : AbstractRiskMultipleCommand {
        
        private readonly MapAreaLabeler _areaLabeler;
        private readonly RiskDataManager dataManager;
        private readonly RiskLogicCore riskLogicCore;

        private readonly string _nameOfArea;
        private readonly int numberOfRecruit;
        private readonly int sizeOfArmiesBeforeRecruit;
        
        public RecruitCommand(string areaName, int recruitSize) {
            _areaLabeler = MapAreaLabeler.instance;
            riskLogicCore = RiskLogicCore.instance;
            dataManager = RiskDataManager.instance;

            _nameOfArea = areaName;
            numberOfRecruit = recruitSize;
            sizeOfArmiesBeforeRecruit = int.Parse(_areaLabeler.GetLabel(areaName));
        }
        
        public override string Description => "Reinforce " + _nameOfArea +" " + numberOfRecruit + " armies.";

        protected override bool ViewUpdateCore() {
            var sizeOfArmiesAfterRecruitStr = (sizeOfArmiesBeforeRecruit + numberOfRecruit).ToString();
            var tmpResult = _areaLabeler.SetLabel(_nameOfArea, sizeOfArmiesAfterRecruitStr);
            return tmpResult;
        }

        protected override bool ModelUpdateCore() {
            return riskLogicCore.Reinforce(_nameOfArea, numberOfRecruit);
        }

        protected override bool ViewUpdateRollbackCore() {
            _areaLabeler.SetLabel(_nameOfArea, sizeOfArmiesBeforeRecruit.ToString());
            return true;
        }

        protected override bool ModelUpdateRollbackCore() {
            return riskLogicCore.Reinforce(_nameOfArea, -numberOfRecruit);
        }
    }
}