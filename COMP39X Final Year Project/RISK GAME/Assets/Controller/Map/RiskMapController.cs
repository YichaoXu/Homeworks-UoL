using UnityEngine;
using WPMF;

namespace Risk.Controller.Map {

    public class MapIgnoreController : AbstractRiskMapController {
        public MapIgnoreController(WorldMap2D mapView) : base(mapView) {
        }

        public MapIgnoreController(AbstractRiskMapController previous) : base(previous) { }

        public override bool HandleOnCountryClick(string countryName) {
            return false;
        }

        public override bool HandleCursorInCountryMovement(string countryName) {
            return false;
        }

        public override bool HandleCursorOutCountryMovement(string countryName) {
            return false;
        }

        public override bool HandleEscapeKeyPress() {
            return false;
        }

    }

    public class MapAttackController : AbstractRiskMapController {

        public MapAttackController(AbstractRiskMapController previous) : base(previous) {
        }

        public override bool HandleOnCountryClick(string countryName) {
            if (mapControlHelper.highlightedNeighboursNameList.Contains(countryName)) {
                mapControlHelper.HidePredicatedTrajectory();
                return mapControlHelper.AddAttackTrajectoryFromHighlightedCountry(countryName);
            }
            else {
                var countryNeighboursNameList = modelDelegate.GetCountryNeighboursNameList(countryName);
                mapControlHelper.CancelHighlight();
                return mapControlHelper.HighlightCountry(countryName, countryNeighboursNameList);
            }
        }

        public override bool HandleCursorInCountryMovement(string countryName) {
            return mapControlHelper.ShowPredicatedTrajectoryFromHighlightedCountry(countryName);
        }

        public override bool HandleCursorOutCountryMovement(string countryName) {
            return mapControlHelper.HidePredicatedTrajectory();
        }

        public override bool HandleEscapeKeyPress() {
            return mapControlHelper.HidePredicatedTrajectory() ||
                   mapControlHelper.HideLastAttackTrajectory() ||
                   mapControlHelper.CancelHighlight();
        }
    }

    public class MapRecruitController : AbstractRiskMapController {
        public MapRecruitController(AbstractRiskMapController previous) : base(previous) {
        }

        public override bool HandleOnCountryClick(string countryName) {
            var countryNeighboursNameList = modelDelegate.GetCountryNeighboursNameList(countryName);
            mapControlHelper.CancelHighlight();
            return mapControlHelper.HighlightCountry(countryName, countryNeighboursNameList);
            
        }

        public override bool HandleCursorInCountryMovement(string countryName) {
            return false;
        }

        public override bool HandleCursorOutCountryMovement(string countryName) {
            return false;
        }

        public override bool HandleEscapeKeyPress() {
            return  mapControlHelper.CancelHighlight();
        }
    }
}