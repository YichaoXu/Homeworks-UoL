namespace RiskSrc.Model.AI.Strategy {
    public class ReinforcementAction :IRiskAction {
        public string ActionKey => targetAreaName;
        public int ArmiesSize { get; set; }
        public string targetAreaName { get; }

        public ReinforcementAction(string areaName, int size) {
            targetAreaName = areaName;
            ArmiesSize = size;
        }
    }

    public class MoveAction:IRiskAction {
        public string ActionKey => FromAreaName+ ToAreaName;
        public int ArmiesSize { get; set; }
        public string FromAreaName { get; }
        public string ToAreaName { get; }

        public MoveAction(string fromName, string toName, int size) {
            ToAreaName = toName;
            FromAreaName = fromName;
            ArmiesSize = size;
        }
    }
}