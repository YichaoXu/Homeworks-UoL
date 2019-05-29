using System.Collections.Generic;
using RiskSrc.Model;
using RiskSrc.Model.Data;
using RiskSrc.Model.IO;
using RiskSrc.View.Map;
using UnityEngine;

namespace RiskSrc.Controller.Command.Unique {
    public class HighlightCommand : AbstractRiskExclusiveCommand {
        private readonly Color? colorForHighlightedArea;
        private readonly Color? colorForHighlightedAreaNeighbours;

        private readonly MapSurfacePainter _mapPainter;

        private readonly string targetHighlightArea;
        private readonly List<string> neighboursNameList;

        public HighlightCommand(string targetArea) {
            targetHighlightArea = targetArea;
            var dataManager = RiskDataManager.instance;
            neighboursNameList = dataManager.GetAreaNeighboursNameList(targetArea);
            var colorSetting = JsonFile.GetJsonFile(FilePath.DecorationSettingAbsPath).Read();
            colorForHighlightedArea = colorSetting["HighlightCountry"].AsColor();
            colorForHighlightedAreaNeighbours = colorSetting["HighlightNeighbour"].AsColor();
            _mapPainter = MapSurfacePainter.instance;
        }

        public override string Description => "Map.Update.AreaHighlight";


        protected override bool ExecuteCore() {
            // 1. Highlight target area;
            _mapPainter.SetAreaColor(
                name:targetHighlightArea, 
                color: colorForHighlightedArea, 
                priority:AreaColorPriority.Highlight
            );
            // 2. Highlight its neighbours;
            neighboursNameList.ForEach(neighbourName => {
                _mapPainter.SetAreaColor(
                    name: neighbourName, 
                    color: colorForHighlightedAreaNeighbours, 
                    priority:AreaColorPriority.Highlight
                );
            });
            return true;
        }

        protected override bool UndoCore() {
            // 1. Cancel Highlight target area;
            _mapPainter.CancelAreaColor(
                name:targetHighlightArea, 
                priority:AreaColorPriority.Highlight
            );
            // 2. Cancel Highlight its neighbours;
            neighboursNameList.ForEach(neighbourName => {
                _mapPainter.CancelAreaColor(
                    name: neighbourName, 
                    priority:AreaColorPriority.Highlight
                );
            });
            return true;
        }
    }
}