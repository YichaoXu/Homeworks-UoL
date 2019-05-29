using System.Collections.Generic;
using UnityEngine;
using WPMF;

namespace RiskSrc.View.Map{
    
    public class MapSurfacePainter {

        private static MapSurfacePainter _instance;

        public static MapSurfacePainter instance => _instance ?? (_instance = new MapSurfacePainter());


        private readonly WorldMap2D mapView;
        private readonly Dictionary<string, PriorityOrderedList<Color?>> areaColorDictionary;

        private MapSurfacePainter() {
            mapView = WorldMap2D.instance;
            areaColorDictionary = new Dictionary<string, PriorityOrderedList<Color?>>();
        }

        public bool SetAreaColor(string name, Color? color, AreaColorPriority priority) {
            if (!areaColorDictionary.ContainsKey(name))
                areaColorDictionary[name] = new PriorityOrderedList<Color?>();
            var priorityStack = areaColorDictionary[name];
            priorityStack.Add(color,(int)priority);
            return updateAreaSurfaceColor(name);
        }

        public Color? GetAreaColor(string name, AreaColorPriority priority) {
            if (!areaColorDictionary.ContainsKey(name)) return null;
            return areaColorDictionary[name]?.GetElement((int)priority);
        }

        public bool SetContinentTexture(string continentName, List<string> areaNamesList, Texture2D texture) {
            areaNamesList.ForEach(areaName => {
                var decorator = new CountryDecorator {
                    isColorized = true, 
                    texture = texture, 
                    textureOffset = Misc.Vector2down * 2.4f
                };
                mapView.decorator.SetCountryDecorator (continentName, areaName, decorator);
            });
            return true;
        }

        public bool CancelContinentTexture(string continentName) {
            var group =  mapView.decorator.GetDecoratorGroup(continentName, false);
            group.decorators.ForEach(decorator => decorator.hidden = true);
            return true;
        }

        public bool CancelAreaColor(string name, AreaColorPriority priority) {
            if (!areaColorDictionary.ContainsKey(name)) return false;
            return areaColorDictionary[name].Remove((int)priority)  
                   && updateAreaSurfaceColor(name);
        }

        private bool updateAreaSurfaceColor(string name) {
            var areaIndex = mapView.GetAreaIndex(name);
            var area = mapView.countries[areaIndex];
            var decorator = mapView.decorator.GetAreaDecorator(area.continent, area.name);
            if (decorator == null) return false;
            
            var priorityStack = areaColorDictionary[name];
            var highestPrioritySetting = priorityStack.GetHighestPriorityElement();
            var highestPriority = highestPrioritySetting?.Item2;
            var highestPriorityColor = highestPrioritySetting?.Item1;
            if (highestPriority == null || highestPriorityColor == null
                || highestPriority == (int)AreaColorPriority.Continent) {
                decorator.isTextureOverwriteColor = true;
            } else {
                decorator.isTextureOverwriteColor = false;
                decorator.fillColor = (Color) highestPriorityColor;
            }
            return true;
        }
    }

    public enum AreaColorPriority {
        Continent,
        Player,
        Highlight
    }
}