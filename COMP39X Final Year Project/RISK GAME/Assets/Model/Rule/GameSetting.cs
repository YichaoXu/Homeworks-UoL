using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class GameSetting {


        private static GameSetting _shared;
        public static GameSetting shared {
            get {
                if (_shared == null)
                    _shared = new GameSetting();
                return _shared;
            }
        }

        private bool? _isSetColourizingMap;
        public bool isSetColourizedMap {
            get {
                if (_isSetColourizingMap == null) {
                    //TODO: Finish read the local database
                    _isSetColourizingMap = true;
                }
                return (bool)_isSetColourizingMap;
            }
            set {
                _isSetColourizingMap = value;
                //TODO: write to local database
            }
        }

        private bool? _isSetPlayingAudio;
        public bool isSetPlayingAudio {
            get {
                if (_isSetPlayingAudio == null) {
                    //TODO: read the local database
                    _isSetPlayingAudio = true;
                }
                return (bool) _isSetPlayingAudio;
            }
            set {
                _isSetPlayingAudio = value;
                //TODO: write to local database
            }
        }

        private Dictionary<string, Color> _continentColours;
        public Dictionary<string, Color> continentColours{
            get {
                if (_continentColours == null) {
                    //TODO: read the local database
                    _continentColours = new Dictionary<string, Color>(6);
                    _continentColours.Add("Aisa", new Color(250f / 255f, 150f / 255f, 50f / 255f));
                    _continentColours.Add("Africa", new Color(250f / 255f, 100f / 255f, 100f / 255f));
                    _continentColours.Add("Europe", new Color(30f / 255f, 100f / 255f, 220f / 255f));
                    _continentColours.Add("North America", new Color(150f / 255f, 200f / 255f, 125f / 255f));
                    _continentColours.Add("South America", new Color(85f / 255f, 107f / 255f, 47f / 255f));
                    _continentColours.Add("Oceania", new Color(150f / 255f, 130f / 255f, 200f / 255f));
                }
                return _continentColours;
            }
        }
    }

}

