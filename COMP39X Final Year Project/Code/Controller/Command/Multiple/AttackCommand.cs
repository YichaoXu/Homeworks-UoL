using System;
using RiskSrc.Controller.Command.Unique;
using RiskSrc.Model;
using RiskSrc.Model.IO;
using RiskSrc.View.Map;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RiskSrc.Controller.Command.Multiple {

    public class AttackCommand : AbstractRiskMultipleCommand {
        private const int priorityForAttackTrajectory = 1;

        private readonly Color? colorForAttacker;
        private readonly Color colorForAttackTrajectory;
        

        private readonly string nameOfAttackerArea;
        private readonly string nameOfDefenderArea;
        private readonly int attackerSize;

        private readonly MapAreaLabeler _areaLabeler;
        private readonly MapSurfacePainter _mapPainter;
        private readonly MapTrajectoryMaker _trajectoryMaker;
        
        private readonly RiskLogicCore riskLogicCore ;
        private readonly RiskDataManager dataManager;

        private GameObject trajectoryBackUp;

        public AttackCommand(string from, string to, int size) {
            nameOfAttackerArea = from;
            nameOfDefenderArea = to;
            attackerSize = size;

            _areaLabeler = MapAreaLabeler.instance;
            _mapPainter = MapSurfacePainter.instance;
            _trajectoryMaker = MapTrajectoryMaker.instance;
            
            riskLogicCore = RiskLogicCore.instance;
            dataManager = RiskDataManager.instance;
            
            var colorSetting = JsonFile.GetJsonFile(FilePath.DecorationSettingAbsPath).Read();
            colorForAttackTrajectory = (Color) colorSetting["AttackTrajectory"].AsColor();
            colorForAttacker = _mapPainter.GetAreaColor(
                name:from, 
                priority: AreaColorPriority.Player
            );
        }

        public override string Description 
            => "attacks from " + nameOfAttackerArea + 
               " to " + nameOfDefenderArea + 
               " with "+ attackerSize + " Armies.";

        protected override bool ViewUpdateCore() {
            var newTrajectory = _trajectoryMaker.CreateTrajectory(
                fromAreaName: nameOfAttackerArea,
                toAreaName: nameOfDefenderArea,
                color: colorForAttackTrajectory,
                arc: 0f
            );
            _trajectoryMaker.SetTrajectory(
                fromName:nameOfAttackerArea,
                toName:nameOfDefenderArea,
                trajectory:newTrajectory,
                priority: priorityForAttackTrajectory
            );
            Object.Destroy(newTrajectory, 3f);
            return true;
        }

        //TODO Finish the attack Model core
        protected override bool ModelUpdateCore() {
            var isAttackerWin = riskLogicCore.Attack(
                attackerAreaName: nameOfAttackerArea,
                defenderAreaName: nameOfDefenderArea,
                attackerSize: attackerSize
            );
            if (isAttackerWin) // If the Attacker win the game
                _mapPainter.SetAreaColor(nameOfDefenderArea, colorForAttacker, AreaColorPriority.Player);
            var attackerAreaArmiesSize = dataManager.GetAreaTotalArmiesSize(nameOfAttackerArea);
            var defenderAreaArmiesSize = dataManager.GetAreaTotalArmiesSize(nameOfDefenderArea);
            return _areaLabeler.SetLabel(nameOfAttackerArea, attackerAreaArmiesSize.ToString()) 
                   && _areaLabeler.SetLabel(nameOfDefenderArea, defenderAreaArmiesSize.ToString());
        }

        protected override bool ViewUpdateRollbackCore() {
            var commandTrajectory = _trajectoryMaker.GetTrajectory(nameOfAttackerArea, nameOfDefenderArea);
            _trajectoryMaker.SetTrajectory(
                fromName:nameOfAttackerArea,
                toName:nameOfDefenderArea,
                trajectory:null,
                priority: priorityForAttackTrajectory
            );
            if (commandTrajectory != null) 
                Object.Destroy(commandTrajectory);          
            return true;
        }
        //TODO HANDLE THE DEBUG

        protected override bool ModelUpdateRollbackCore() {
            throw new NotImplementedException();
        }
    }
}