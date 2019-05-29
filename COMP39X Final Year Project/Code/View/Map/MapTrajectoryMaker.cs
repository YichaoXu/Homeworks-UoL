using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WPMF;

namespace RiskSrc.View.Map {
    public class MapTrajectoryMaker {
        private static MapTrajectoryMaker _instance;
        public static MapTrajectoryMaker instance => _instance ?? (_instance = new MapTrajectoryMaker());

        private readonly WorldMap2D mapView;
        private readonly Dictionary<string, PriorityOrderedList<GameObject>> trajectoryDictionary;

        private MapTrajectoryMaker() {
            mapView = WorldMap2D.instance;
            trajectoryDictionary = new Dictionary<string, PriorityOrderedList<GameObject>>();
        }
        
        public GameObject GetTrajectory(string fromName, string toName, int priority) {
            return trajectoryDictionary[fromName + toName]?.GetElement(priority);
        }

        public GameObject GetTrajectory(string fromName, string toName) {
            return trajectoryDictionary[fromName + toName]?.GetHighestPriorityElement()?.Item1;
        }

        public void SetTrajectory(string fromName, string toName, GameObject trajectory, int priority) {
            var key = fromName + toName;
            if (!trajectoryDictionary.ContainsKey(key)) 
                trajectoryDictionary[key] = new PriorityOrderedList<GameObject>();
            var priorityStack = trajectoryDictionary[key];
            var previousHighestTrajectory = priorityStack.GetHighestPriorityElement()?.Item1;
            if (previousHighestTrajectory != null) 
                previousHighestTrajectory.SetActive(false);
            priorityStack.Add(trajectory,priority);
            var currentHighestTrajectory = priorityStack.GetHighestPriorityElement()?.Item1;
            if (currentHighestTrajectory != null)
                currentHighestTrajectory.SetActive(true);
        }

        public GameObject CreateTrajectory(string fromAreaName, string toAreaName, Color color, float arc) {
            int startIndex = mapView.GetAreaIndex(fromAreaName);
            int endIndex = mapView.GetAreaIndex(toAreaName);
            var start = mapView.countries[startIndex].center;
            var end = mapView.countries[endIndex].center;
            if (startIndex < 0 || endIndex < 0) return null;
            var newTrajectory = mapView.AddLine(
                start: start,
                end: end,
                color: color,
                arcElevation: arc,
                duration: 0.5f,
                lineWidth: 0.5f,
                fadeOutAfter: 0.0f
            );
            newTrajectory.SetActive(false);
            return newTrajectory;
        }
    }
}