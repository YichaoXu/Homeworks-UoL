using SimpleJSON;

namespace Risk.Model.Rule {
    public class Country {
        
        public readonly int id;
        public readonly string name;
        public int armiesSize { get; private set; }
        private string ownerName;

        public Country(JSONNode rawData) {
            id = rawData["id"];
            name = rawData["name"];
            armiesSize = 2; //TODO Finish 
        }

        public bool Reinforcement(int number) {
            armiesSize += number;
            return true;
        }

        public bool RemoveArmies(int number) {
            if (armiesSize <= number) 
                armiesSize = 0;
            else 
                armiesSize -= number;
            return true;
        }
    }

}