using Risk.Controller.Map.Helper;
using Risk.Model;
using WPMF;

namespace Risk.Controller.Map {

    public abstract class AbstractRiskMapController {

        protected ModelDelegateInterface modelDelegate;
        protected AbstractRiskMapControlHelper mapControlHelper;

        protected AbstractRiskMapController(WorldMap2D mapView) : this() {
            mapControlHelper = new ColorizedMapControlHelper(mapView);
        }

        protected AbstractRiskMapController(AbstractRiskMapController previous) : this() {
            mapControlHelper = previous.mapControlHelper;
        }

        private AbstractRiskMapController() {
            modelDelegate = RiskModelDelegate.instance;
        }

        public bool MoveToNextTurn() {
            mapControlHelper.HidePredicatedTrajectory();
            mapControlHelper.HideAllAttackTrajectory();
            mapControlHelper.CancelHighlight();
            return true;
        }


        public abstract bool HandleOnCountryClick(string countryName);
        public abstract bool HandleCursorInCountryMovement(string countryName);
        public abstract bool HandleCursorOutCountryMovement(string countryName);
        public abstract bool HandleEscapeKeyPress();

    }
}
