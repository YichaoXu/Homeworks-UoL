using SimpleJSON;

namespace Assets.Model {
    public class Continent {
        private string name;
        private int additionalBonus;
        private readonly int[] countriesIdArray;

        public Continent(JSONNode rawData) {
            name = rawData["name"];
            additionalBonus = rawData["bonus"].AsInt;
            var tmpArray = rawData["countries"].AsArray;
            countriesIdArray = new int[tmpArray.Count];
            for (int i = 0; i <= tmpArray.Count; i++) 
                countriesIdArray[i] = tmpArray[i];
        }
    }
}
