using System.Runtime.InteropServices;
using RiskSrc.Model;
using RiskSrc.View.Map;

namespace RiskSrc.Controller.Command.Multiple {
    public class MoveCommand:AbstractRiskMultipleCommand {
        private readonly int armiesSize;
        private readonly string toAreaName;
        private readonly string fromAreaName;

        private readonly MapAreaLabeler _areaLabeler;

        private readonly RiskLogicCore logicCore;
        private readonly RiskDataManager dataManager;

        private int toAreaArmiesSizeBeforeUpdate;
        private int fromAreaArmiesSizeBeforeUpdate;


        public override string Description => 
            " moves "+ armiesSize +" armies from " +fromAreaName + " to " + toAreaName;

        public MoveCommand(string from, string to, int size) {
            armiesSize = size;
            toAreaName = to;
            fromAreaName = from;

            _areaLabeler = MapAreaLabeler.instance;
            
            logicCore = RiskLogicCore.instance;
            dataManager = RiskDataManager.instance;
        }
        protected override bool ViewUpdateCore() {
            toAreaArmiesSizeBeforeUpdate = dataManager.GetAreaTotalArmiesSize(toAreaName);
            var toAreaArmiesSizeAfterUpdate = toAreaArmiesSizeBeforeUpdate + armiesSize;
            
            _areaLabeler.SetLabel(
                onAreaName:toAreaName, 
                labelStr:toAreaArmiesSizeAfterUpdate.ToString()
            );
            
            fromAreaArmiesSizeBeforeUpdate = dataManager.GetAreaTotalArmiesSize(fromAreaName);
            var fromAreaArmiesSizeAfterUpdate = fromAreaArmiesSizeBeforeUpdate - armiesSize;
            _areaLabeler.SetLabel(
                onAreaName:fromAreaName, 
                labelStr:fromAreaArmiesSizeAfterUpdate.ToString()
            );
            return true;
        }

        protected override bool ViewUpdateRollbackCore() {
            _areaLabeler.SetLabel(
                onAreaName:toAreaName, 
                labelStr:toAreaArmiesSizeBeforeUpdate.ToString()
            );
            
            _areaLabeler.SetLabel(
                onAreaName:fromAreaName, 
                labelStr:toAreaArmiesSizeBeforeUpdate.ToString()
            );
            return true;
        }
        
        protected override bool ModelUpdateCore () {
            return logicCore.Move(
                fromName: fromAreaName,
                toName: toAreaName,
                armiesSize: armiesSize
            );
        }

        protected override bool ModelUpdateRollbackCore() {
            var currentFromAreaArmiesSize = dataManager.GetAreaTotalArmiesSize(fromAreaName);
            var currentToAreaArmiesSize = dataManager.GetAreaTotalArmiesSize(toAreaName);
            if (currentFromAreaArmiesSize == fromAreaArmiesSizeBeforeUpdate - armiesSize 
                && currentToAreaArmiesSize == toAreaArmiesSizeBeforeUpdate + armiesSize) {
                return logicCore.Move(
                    fromName: toAreaName,
                    toName: fromAreaName,
                    armiesSize: armiesSize
                );
            }
            return false;
        }
    }
}