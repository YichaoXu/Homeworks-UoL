using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WPMF;

namespace RiskSrc.View.Map {
    public class MapAreaLabeler {
        private static MapAreaLabeler _instance;
        public static MapAreaLabeler instance => _instance ?? (_instance = new MapAreaLabeler());

        private readonly WorldMap2D mapView;
        private readonly RiskViewSearcher viewSearcher;
        private readonly Dictionary<string, string> areaTextLabelDictionary;
        private readonly Dictionary<string, GameObject> areaIconLabelDictionary;

        private MapAreaLabeler(){
            mapView = WorldMap2D.instance;
            viewSearcher = RiskViewSearcher.instance;
            areaTextLabelDictionary = new Dictionary<string, string>();
            areaIconLabelDictionary = new Dictionary<string, GameObject>();
        }
        
        public bool SetLabel(string onAreaName, string labelStr) {
            var areaIndex = mapView.GetAreaIndex(onAreaName);
            var area = mapView.countries[areaIndex];
            var notification = new GameObject();
            notification.AddComponent<TextMeshPro>();
            notification.transform.position = CalculateAreaCenter3DLocation(
                areaCenter: area.center,
                deviation: null
            );
            var notificationText = notification.GetComponent<TextMeshPro>();
            notificationText.fontSize = 30;
            notificationText.fontStyle = FontStyles.Bold;
            notificationText.font = TMP_FontAsset.defaultFontAsset;
            if (areaTextLabelDictionary.ContainsKey(onAreaName))
                notificationText.text = areaTextLabelDictionary[onAreaName] + "->" + labelStr;
            Object.Destroy(notification, 1.5f);

            var areaDecorator = mapView.decorator.GetAreaDecorator(
                groupName: area.continent, 
                areaName: onAreaName
            );
            areaDecorator.customLabel = labelStr;
            areaTextLabelDictionary[onAreaName] = labelStr;//Record the value of text label
            AddIconLabel(area, areaDecorator);
            return true;
        }

        public string GetLabel(string onAreaName)
        {
            return (areaTextLabelDictionary.ContainsKey(onAreaName))
                ? areaTextLabelDictionary[onAreaName]
                : onAreaName;
        }

        public bool SetAllAreasLabel(Dictionary<string, string> labelsPairDictionary) {
            foreach (var labelPair in labelsPairDictionary) {
                var areaName = labelPair.Key;
                var labelStr = labelPair.Value;
                var areaIndex = mapView.GetAreaIndex(areaName);
                var area = mapView.countries[areaIndex];
                var areaDecorator = mapView.decorator.GetAreaDecorator(
                    groupName: area.continent, 
                    areaName: areaName
                );
                areaDecorator.customLabel = labelStr;
                areaTextLabelDictionary[areaName] = labelStr;
                AddIconLabel(area, areaDecorator);
            }
            return true;
        }
        
        public Dictionary<string, string> GetAllAreasLabel(){
            return areaTextLabelDictionary;
        }

        private Vector3 CalculateAreaCenter3DLocation(Vector2 areaCenter, Vector3? deviation) {
            var transform = mapView.transform;
            var localScale = transform.localScale;
            var text3DLocation = new Vector3(
                x: localScale.x * areaCenter.x,
                y: localScale.y * areaCenter.y,
                z: transform.position.z
            );
            if (deviation != null) text3DLocation += deviation.Value;
            return text3DLocation;
        }

        private void AddIconLabel(Country area, CountryDecorator decorator) {
            var areaName = area.name;
            var areaColor = decorator.fillColor;
            if (areaColor == Color.red) return;

            if (areaIconLabelDictionary.ContainsKey(areaName)) {
                var backupIcon = areaIconLabelDictionary[areaName];
                var backupIconColor = backupIcon.GetComponent<SpriteRenderer>()?.color;
                if (areaColor == backupIconColor) return;
                Object.Destroy(areaIconLabelDictionary[areaName]);
            }

            var iconSample =  viewSearcher.GetMapIconByName(
                areaColor == Color.white? "Barbarian": "Army"
            ); 
            var icon = Object.Instantiate(iconSample);
            icon.transform.position = CalculateAreaCenter3DLocation(
                areaCenter:area.center, 
                deviation: new Vector3(5, 0, 0)
            );
            icon.GetComponent<SpriteRenderer>().color = areaColor;
            icon.SetActive(true);
            areaIconLabelDictionary[areaName] = icon;
        }
    }
}