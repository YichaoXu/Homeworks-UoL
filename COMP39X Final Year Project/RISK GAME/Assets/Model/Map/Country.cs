using SimpleJSON;

namespace Risk.Model.Rule {
    public class Country {
        public readonly int id;
        public readonly string name;
        private int armiesSize;
        private string ownerName;

        public Country(JSONNode rawData) {
            id = rawData["id"];
            name = rawData["name"];
            armiesSize = 0;
        }

        public void Reinforcement(int number) {
            armiesSize += number;
        }

        public bool UnderAttack(Country attacker, int attackerSize) {
            if (attackerSize > armiesSize) return false;
            
            if (attackerSize < )
        }
    }

    class Battle {
        
    }
}