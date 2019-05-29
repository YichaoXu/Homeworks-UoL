namespace RiskSrc.Model.AI.Strategy {
    public interface IRiskAction {
        string ActionKey { get; }
        int ArmiesSize { get; set; }
    }
}