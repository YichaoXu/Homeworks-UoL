using WPMF;
using UnityEngine;
using System.Collections.Generic;

namespace Risk.Controller.Map.Helper
{
    public class ColorizedMapControlHelper : AbstractRiskMapControlHelper {

        private static readonly Dictionary<string, Color> DEFAULT_CONTINENT_COLOR_SETTING
            = new Dictionary<string, Color>(6) {
                { "Asia", new Color(250f / 255f, 150f / 255f, 50f / 255f) },
                { "Africa", new Color(250f / 255f, 100f / 255f, 100f / 255f) },
                { "Europe", new Color(30f / 255f, 100f / 255f, 220f / 255f) },
                { "North America", new Color(150f / 255f, 200f / 255f, 125f / 255f) },
                { "South America", new Color(85f / 255f, 107f / 255f, 47f / 255f) },
                { "Oceania", new Color(150f / 255f, 130f / 255f, 200f / 255f) }
        };

        public Dictionary<string, Color> continentColorSetting { private get; set; }

        public ColorizedMapControlHelper(AbstractRiskMapControlHelper previous) : base(previous){
            continentColorSetting = DEFAULT_CONTINENT_COLOR_SETTING;
            ColorizeAllContinent();
        }

        public ColorizedMapControlHelper(WorldMap2D map): base(map) {
            // 1. Set default color for each continent.
            mapView = map;
            continentColorSetting = DEFAULT_CONTINENT_COLOR_SETTING;
            ColorizeAllContinent();
        }

        public override bool CancelHighlight() {
            if (highlightedCountryName == null)  return false;
            var hightlightedCountriesNameList = new List<string>(highlightedNeighboursNameList);
            hightlightedCountriesNameList.Add(highlightedCountryName);
            hightlightedCountriesNameList.ForEach((countryName) => {
                var continentName = mapView.countries[mapView.GetCountryIndex(countryName)].continent;
                var continentColor = continentColorSetting[continentName];
                mapView.ToggleCountrySurface(countryName, true, continentColor);
            });
            highlightedCountryName = null;
            highlightedNeighboursNameList = new List<string>();
            return true;
        }

        private void ColorizeAllContinent() {
            if (continentColorSetting == null) return;
            foreach (var keyPair in continentColorSetting) {
                string continentName = keyPair.Key;
                Color continentColor = keyPair.Value;
                mapView.ToggleContinentSurface(continentName, true, continentColor);
            }
        }
    }

    public class NaturalMapControlHelper : AbstractRiskMapControlHelper {

        public NaturalMapControlHelper(AbstractRiskMapControlHelper previous) : base(previous) {
            mapView.HideCountrySurfaces();
        }

        public NaturalMapControlHelper(WorldMap2D mapView): base(mapView) {
            this.mapView = mapView;
            mapView.HideCountrySurfaces();
        }

        public override bool CancelHighlight() {
            if (highlightedCountryName == null) return false;
            mapView.HideCountrySurface(mapView.GetCountryIndex(highlightedCountryName));
            highlightedCountryName = null;
            highlightedNeighboursNameList.ForEach((countryName) => {
                mapView.HideCountrySurface(mapView.GetCountryIndex(countryName));
            });
            highlightedNeighboursNameList = new List<string>();
            return true;
        }
    }
}

