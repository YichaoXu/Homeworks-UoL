using UnityEngine;
using WPMF;
/*
 * Thees Classes are the controller of the gameMap
 * It will control the color and titles of each country on map
 */

namespace Risk.Controller.Map {
    /*
     * The class is the Map controller in the initiate phase
     */
    public class MapInitiateController : AbstractRiskMapController {
        /*Constructor*/
        public MapInitiateController(WorldMap2D mapView) : base(mapView) {}
        
        public MapInitiateController(AbstractRiskMapController previous) : base(previous) {
            //all objects and highlight will be removed
            mapDecorator.HidePredicatedTrajectory();
            mapDecorator.HideAllAttackTrajectory();
            mapDecorator.CancelHighlight();
        }

        /*In this phase, we do not need the class to do anything else*/
        public override bool HandleOnCountryClick(string countryName) { return false; }

        public override bool HandleCursorInCountryMovement(string countryName) { return false; }

        public override bool HandleCursorOutCountryMovement(string countryName) { return false; }

        public override bool HandleEscapeKeyPress() { return false; }

    }

    /*
     * The class is the Map controller in the attack phase
     */
    public class MapAttackController : AbstractRiskMapController {

        public MapAttackController(AbstractRiskMapController previous) : base(previous) {  }

        /*Handle click event, it will highlight the clicked country or add a trajectory between two country*/
        public override bool HandleOnCountryClick(string countryName) {
            if (mapDecorator.highlightedNeighboursNameList.Contains(countryName)) {
                mapDecorator.HidePredicatedTrajectory();
                return mapDecorator.AddAttackTrajectoryFromHighlightedCountry(countryName);
            }
            else {
                var countryNeighboursNameList = modelDelegate.GetCountryNeighboursNameList(countryName);
                mapDecorator.CancelHighlight();
                return mapDecorator.HighlightCountry(countryName, countryNeighboursNameList);
            }
        }
        
        /*Handle the cursor in country movement event*/
        public override bool HandleCursorInCountryMovement(string countryName) {
            return mapDecorator.ShowPredicatedTrajectoryFromHighlightedCountry(countryName);
        }

        public override bool HandleCursorOutCountryMovement(string countryName) {
            return mapDecorator.HidePredicatedTrajectory();
        }

        public override bool HandleEscapeKeyPress() {
            return mapDecorator.HidePredicatedTrajectory() ||
                   mapDecorator.HideLastAttackTrajectory() ||
                   mapDecorator.CancelHighlight();
        }
    }

    /*
     * The class is the Map controller in the Recruit phase
     */
    public class MapRecruitController : AbstractRiskMapController {

        private string currentSelectedCountryName;
        public MapRecruitController(AbstractRiskMapController previous) : base(previous) {
        }

        public override bool HandleOnCountryClick(string countryName) {
            currentSelectedCountryName = countryName;
            var countryNeighboursNameList = modelDelegate.GetCountryNeighboursNameList(countryName);
            mapDecorator.CancelHighlight();
            return mapDecorator.HighlightCountry(countryName, countryNeighboursNameList);
        }

        public void UpdateCountryArmies() {
            var armySize = modelDelegate.GetCountryArmiesSize(currentSelectedCountryName);
            Debug.Log(armySize);
            Debug.Log(currentSelectedCountryName);
            mapDecorator.SetCountryLabel(currentSelectedCountryName, armySize.ToString());
            mapDecorator.UpdateCountryLabel();
            currentSelectedCountryName = null;
        }

        public override bool HandleCursorInCountryMovement(string countryName) {
            return false;
        }

        public override bool HandleCursorOutCountryMovement(string countryName) {
            return false;
        }

        public override bool HandleEscapeKeyPress() {
            return  mapDecorator.CancelHighlight();
        }
    }
}