using SimpleJSON;

namespace RiskSrc.Model.Data {
    public class AbstractData {
        
        public readonly int ID;
        public readonly string Name;
        
        public AbstractData(JSONNode rawData) {
            ID = rawData["ID"].AsInt;
            Name = rawData["Name"].Value;
        }
    }
}