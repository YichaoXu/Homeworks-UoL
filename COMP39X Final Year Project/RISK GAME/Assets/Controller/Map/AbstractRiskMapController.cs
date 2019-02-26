using System.Collections.Generic;
using Risk.Controller.Map.Helper;
using Risk.Model;
using UnityEngine;
using UnityEngine.UI;
using WPMF;

namespace Risk.Controller.Map {

    public abstract class AbstractRiskMapController {
        
        protected ModelDelegateInterface modelDelegate;
        protected AbstractMapDecorator mapDecorator;

        protected AbstractRiskMapController(WorldMap2D mapView) {
            modelDelegate = RiskModelDelegate.instance;
            mapDecorator = new ColorizedMapDecorator(mapView);
            var countryArmiesSizeDic = modelDelegate.GetAllCountriesArmiesSize();

            foreach (var countryArmiesSize in countryArmiesSizeDic) {
                mapDecorator.SetCountryLabel(countryArmiesSize.Key, countryArmiesSize.Value.ToString());
            }
                
            mapView.showCountryNames = true;
        }

        protected AbstractRiskMapController(AbstractRiskMapController previous) {
            modelDelegate = previous.modelDelegate;
            mapDecorator = previous.mapDecorator;
        }

        
        public abstract bool HandleOnCountryClick(string countryName);
        public abstract bool HandleCursorInCountryMovement(string countryName);
        public abstract bool HandleCursorOutCountryMovement(string countryName);
        public abstract bool HandleEscapeKeyPress();

    }
}
