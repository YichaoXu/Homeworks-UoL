using SimpleJSON;

namespace RiskSrc.Model.Data {
    public class PlayerData: AbstractData {
        
        public readonly int BasicRecruit;
        public readonly string Type;
        
        public PlayerData(JSONNode rawData):base(rawData) {
            BasicRecruit = rawData["Recruit"];
            Type = rawData["Type"];
        }
    }
    
    public class ContinentData: AbstractData {

        public readonly int Bonus;   
        public ContinentData(JSONNode rawData):base(rawData) {
            Bonus = rawData["Bonus"];
        }
    }
    
    public class AreaData:AbstractData {

        public int ActiveArmiesSize;
        public int InactiveArmiesSize;
        
        public int TotalArmiesSize => ActiveArmiesSize + InactiveArmiesSize;
        
        public AreaData(JSONNode rawData):base(rawData) {
            /*InactiveArmiesSize = rawData["ArmiesSize"];*/
            InactiveArmiesSize = 2;
        }
    }
}