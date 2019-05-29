using System;
using System.Collections.Generic;
using RiskSrc.View.Component;
using UnityEngine;

namespace RiskSrc.View {
    public class RiskViewSearcher: MonoBehaviour {
        public static RiskViewSearcher instance;

        [SerializeField] private GameObject GameUI;
        [SerializeField] private GameObject MapICONContainer;
        
        private readonly Dictionary<string, GameObject> iconCache = new Dictionary<string, GameObject>();
        private readonly Dictionary<Type, IRiskViewPanel> componentCache = new Dictionary<Type, IRiskViewPanel>();

        private void Awake() {
            instance = this;
        }

        public T GetComponentByName<T>(string componentName) where T : IRiskViewPanel {
            if (componentCache.ContainsKey(typeof(T)) && componentCache[typeof(T)].isUpToDate) {
                return (T) componentCache[typeof(T)];
            }

            var componentCore = FindChildObjectByName(fromObject: GameUI, componentName);
            var parameter = new object[] {componentCore};
            componentCache[typeof(T)] = (T) Activator.CreateInstance(typeof(T), parameter);
            return (T) componentCache[typeof(T)];
        }

        public GameObject GetMapIconByName(string iconName) {
            return iconCache.ContainsKey(iconName) 
                ? iconCache[iconName] 
                : FindChildObjectByName(fromObject: MapICONContainer, iconName);
        }

        private GameObject FindChildObjectByName(GameObject fromObject, string objectName) {
            var gameObjectNumber = fromObject.transform.childCount;
            for (var index = 0; index < gameObjectNumber; index++) {
                var anyChildrenObject = fromObject.transform.GetChild(index).gameObject;
                if (anyChildrenObject.name == objectName) return anyChildrenObject;
            }
            return null;
        }
    }
}
