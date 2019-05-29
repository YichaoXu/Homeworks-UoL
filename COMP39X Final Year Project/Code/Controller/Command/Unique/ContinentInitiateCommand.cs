using System.Collections.Generic;
using RiskSrc.Model;
using RiskSrc.Model.IO;
using RiskSrc.View.Map;
using UnityEngine;

namespace RiskSrc.Controller.Command.Unique {
    public class ContinentInitiateCommand : AbstractRiskExclusiveCommand {
        
        private readonly RiskDataManager dataManager;
        private readonly MapSurfacePainter _mapPainter;

        private readonly List<string> continentsNameList;
        public ContinentInitiateCommand() {
            _mapPainter = MapSurfacePainter.instance;
            dataManager = RiskDataManager.instance;
            continentsNameList = new List<string>();
        }

        public override string Description => "Map.Initiate.Continent";

        protected override bool ExecuteCore() {
            var tmpRst = true;
            var decorationSettingFile = JsonFile.GetJsonFile(FilePath.DecorationSettingAbsPath);
            var continentTextureSetting = decorationSettingFile.Read()["Continents"];
            foreach (var textureSetting in continentTextureSetting) {
                var continentName = textureSetting.Key;
                var continentTexture = textureSetting.Value.AsTexture2D();
                var areaNamesList = dataManager.GetContinentAreasNameList(continentName);
                tmpRst &= _mapPainter.SetContinentTexture(
                    continentName: continentName, 
                    areaNamesList: areaNamesList,
                    texture:continentTexture
                );
                continentsNameList.Add(continentName);
            }
            return tmpRst;
        }

        protected override bool UndoCore() {
            return continentsNameList.TrueForAll(
                _mapPainter.CancelContinentTexture
            );
        }
    }
}