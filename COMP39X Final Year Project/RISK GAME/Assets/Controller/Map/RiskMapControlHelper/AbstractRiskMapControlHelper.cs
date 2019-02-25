using WPMF;
using UnityEngine;
using System.Collections.Generic;


namespace Risk.Controller.Map.Helper {
    public abstract class AbstractRiskMapControlHelper{
        protected WorldMap2D mapView;

        public string highlightedCountryName { protected set; get; }
        public List<string> highlightedNeighboursNameList { protected set; get; }

        public Color colorForEnemy { protected get; set; }
        public Color colorForPlayer { protected get; set; }
        public Color colorForAttackTrajectory { protected get; set; }
        public Color colorForPredicatedTrajectory { protected get; set; }
        public Color colorForHighlightedCountry { protected get; set; }
        public Color colorForHighlightedCountryNeighbours { protected get; set; }

        protected GameObject predicatedTrajectory;
        protected List<GameObject> attackTrajectoriesList;

        protected AbstractRiskMapControlHelper(AbstractRiskMapControlHelper previous) {
            mapView = previous.mapView;

            highlightedCountryName = previous.highlightedCountryName;
            highlightedNeighboursNameList = previous.highlightedNeighboursNameList;
            colorForEnemy = previous.colorForEnemy;
            colorForPlayer = previous.colorForPlayer;
            colorForAttackTrajectory = previous.colorForAttackTrajectory;
            colorForPredicatedTrajectory = previous.colorForPredicatedTrajectory;
            colorForHighlightedCountry = previous.colorForHighlightedCountry;
            colorForHighlightedCountryNeighbours = previous.colorForHighlightedCountryNeighbours;

            predicatedTrajectory = previous.predicatedTrajectory;
            attackTrajectoriesList = previous.attackTrajectoriesList;
        }

        protected AbstractRiskMapControlHelper(WorldMap2D map) {
            // 1. Initiate the fields.
            mapView = map;
            highlightedCountryName = null;
            highlightedNeighboursNameList = new List<string>();

            // 2. Set color
            // 2.1 Set default color for the players.
            colorForPlayer = new Color(245f / 255f, 245f / 255f, 245f / 255f);
            colorForEnemy = new Color(112f / 255f, 128f / 255f, 144f / 255f);
            // 2.2 Set default color for Trajectory line.
            colorForPredicatedTrajectory = new Color(0f / 255f, 112f / 255f, 187f / 255f);
            colorForAttackTrajectory = new Color(128f / 255f, 0f, 0f);
            // 2.3 Set default color for highlighted country and its neighbours.
            colorForHighlightedCountry = new Color(255f / 255f, 0f, 0f);
            colorForHighlightedCountryNeighbours = new Color(211f / 255f, 211f / 255f, 211f / 255f);

            predicatedTrajectory = null;
            attackTrajectoriesList = new List<GameObject>();
        }

        public bool HighlightCountry(string targetCountryName, List<string> neighboursNameList) {
            if (targetCountryName == null) return false;
            mapView.ToggleCountrySurface(targetCountryName, true, colorForHighlightedCountry);
            highlightedCountryName = targetCountryName;
            neighboursNameList.ForEach((countryName) => {
                mapView.ToggleCountrySurface(countryName, true, colorForHighlightedCountryNeighbours);
            });
            highlightedNeighboursNameList = neighboursNameList;
            return true;
        }

        public abstract bool CancelHighlight();

        public bool ShowPredicatedTrajectoryFromHighlightedCountry(string toNeighbourName) {
            if (highlightedCountryName == null) return false;
            if (!highlightedNeighboursNameList.Contains(toNeighbourName)) return false;
            HidePredicatedTrajectory();
            var start = mapView.GetCountryCentreVector(highlightedCountryName);
            var end = mapView.GetCountryCentreVector(toNeighbourName);
            const float arcElevation = 0.05f, duration = 0.2f, lineWidth = 1.0f, fadeAfter = 0.0f;
            predicatedTrajectory = mapView.AddLine(start, end, colorForPredicatedTrajectory, arcElevation, duration,
                lineWidth, fadeAfter);
            return true;
        }

        public bool HidePredicatedTrajectory() {
            if (predicatedTrajectory == null) return false;
            Object.Destroy(predicatedTrajectory);
            return true;
        }
        
        public bool AddAttackTrajectoryFromHighlightedCountry(string toNeighbourName) {
            if (highlightedCountryName == null) return false;
            if (!highlightedNeighboursNameList.Contains(toNeighbourName)) return false;
            var newTrajectory = AddTrajectory(new TrajectoryParameter {
                fromCountryName = highlightedCountryName,
                toCountryName = toNeighbourName,
                color = colorForAttackTrajectory,
                arcElevation = 0.0f,
                duration = 0.5f,
                fadeAfter = 0.0f,
                lineWidth = 1.5f,
            });
            attackTrajectoriesList.Add(newTrajectory);
            return true;
        }

        public bool HideLastAttackTrajectory() {
            if (attackTrajectoriesList.Count == 0) return false;
            int lastTrajectoryIndex = attackTrajectoriesList.Count - 1;
            GameObject lastTrajectory = attackTrajectoriesList[lastTrajectoryIndex];
            attackTrajectoriesList.RemoveAt(lastTrajectoryIndex);
            Object.Destroy(lastTrajectory);
            return true;
        }

        public bool HideAllAttackTrajectory() {
            if (attackTrajectoriesList.Count == 0) return false;
            attackTrajectoriesList.ForEach(Object.Destroy);
            return true;
        }

        private GameObject AddTrajectory(TrajectoryParameter parameter) {
            var start = mapView.GetCountryCentreVector(parameter.fromCountryName);
            var end = mapView.GetCountryCentreVector(parameter.toCountryName);
            return mapView.AddLine(
                start, end,
                parameter.color,
                parameter.arcElevation,
                parameter.duration,
                parameter.lineWidth,
                parameter.fadeAfter
            );
        }

        private struct TrajectoryParameter {
            public string fromCountryName;
            public string toCountryName;
            public Color color;
            public float arcElevation;
            public float duration;
            public float lineWidth;
            public float fadeAfter;
        }
    }
}