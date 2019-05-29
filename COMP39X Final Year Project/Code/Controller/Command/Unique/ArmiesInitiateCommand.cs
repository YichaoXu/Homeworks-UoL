using System.Collections.Generic;
using RiskSrc.Model;
using RiskSrc.View.Map;

namespace RiskSrc.Controller.Command.Unique {
    public class ArmiesInitiateCommand : AbstractRiskExclusiveCommand {
        private static ArmiesInitiateCommand currentCommand; 
        
        private readonly Dictionary<string, string> areasLabelsDictionary;

        private readonly Dictionary<string, string> areaLabelsBackup;

        private readonly MapAreaLabeler _areaLabeler;

        public ArmiesInitiateCommand() {
            _areaLabeler = MapAreaLabeler.instance;
            areasLabelsDictionary = new Dictionary<string, string>();
            var areaArmiesDictionary = RiskDataManager.instance.GetAllAreasTotalArmiesSize();
            foreach (var areaArmiesPair in areaArmiesDictionary) 
                areasLabelsDictionary.Add(areaArmiesPair.Key, areaArmiesPair.Value.ToString());
            areaLabelsBackup = new Dictionary<string, string>();
        }


        public override string Description => "Map.Initiate.ArmiesLabel";

        protected override bool ExecuteCore() {
            foreach (var labelPair in _areaLabeler.GetAllAreasLabel()) 
                areaLabelsBackup[labelPair.Key] = labelPair.Value;
            return _areaLabeler.SetAllAreasLabel(areasLabelsDictionary);
        }

        protected override bool UndoCore() {
            return _areaLabeler.SetAllAreasLabel(areaLabelsBackup);
        }
    }
}