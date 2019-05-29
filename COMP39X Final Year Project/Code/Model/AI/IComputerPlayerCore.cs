using RiskSrc.Model.AI.Strategy;

namespace RiskSrc.Model.AI {
    public interface IComputerPlayerCore {
        void Observation();
        RiskStrategy<ReinforcementAction> DeployPhaseReasoning();
        RiskStrategy<MoveAction> AttackPhaseReasoning();
        RiskStrategy<MoveAction> SchedulePhaseReasoning();
    }

    
}