using RiskSrc.Model.IO;
using RiskSrc.View.Map;
using UnityEngine;

namespace RiskSrc.Controller.Command.Unique {
    public class MapAttackPreviewCommand : AbstractRiskExclusiveCommand {
        private readonly Color colorForAttackPreviewTrajectory;

        private const int priorityForPreviewTrajectory = 0;
        
        private readonly string fromAreaName;
        private readonly string toAreaName;
        private readonly MapTrajectoryMaker _trajectoryMaker;

        public MapAttackPreviewCommand(string fromAreaName, string toAreaName) {
            this.fromAreaName = fromAreaName;
            this.toAreaName = toAreaName;
            _trajectoryMaker = MapTrajectoryMaker.instance;
            var colorSetting = JsonFile.GetJsonFile(FilePath.DecorationSettingAbsPath).Read();
            colorForAttackPreviewTrajectory = (Color) colorSetting["AttackPreviewTrajectory"].AsColor();
        }

        public override string Description => "Map.Update.AttackPreview";


        protected override bool ExecuteCore() {
            var newTrajectory = _trajectoryMaker.CreateTrajectory(
                fromAreaName: fromAreaName,
                toAreaName: toAreaName,
                color: colorForAttackPreviewTrajectory, 
                arc: 0.05f
            );
            
            _trajectoryMaker.SetTrajectory(
                fromName: fromAreaName, 
                toName:toAreaName, 
                trajectory:newTrajectory,
                priority:priorityForPreviewTrajectory
            );
            return true;
        }

        protected override bool UndoCore() {
            var commandTrajectory = _trajectoryMaker.GetTrajectory(
                fromName:fromAreaName, 
                toName:toAreaName, 
                priority:priorityForPreviewTrajectory
            );
            _trajectoryMaker.SetTrajectory(
                fromName:fromAreaName,
                toName:toAreaName, 
                trajectory:null,
                priority:priorityForPreviewTrajectory
            );
            if (commandTrajectory != null)
                Object.Destroy(commandTrajectory);
            return true;
        }
    }
}