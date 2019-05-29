using RiskSrc.Controller.Command;
using RiskSrc.Controller.HumanPlayer.Phase;
using RiskSrc.Model;

namespace RiskSrc.Controller.HumanPlayer.Map {
    
    public class TurnStartPhaseControllerCore: AbstractHumanPlayerPhaseControllerCore {
        protected override void HandleOnAreaClick(string clickedCountryName) {
        }

        protected override void HandleOnAreaEnter(string countryName) {
        }

        protected override void HandleOnEscPress() {
        }

        public TurnStartPhaseControllerCore(string playerName) : base(playerName) {
        }
    }
    
    public class TurnEndPhaseControllerCore: AbstractHumanPlayerPhaseControllerCore{

        public TurnEndPhaseControllerCore(string playerName) : base(playerName) {
        }

        protected override void WorkCore() {
            RiskCommandInvoker.instance.CommitAllTransactionAndUpdateCheckpoint();
            RiskLogicCore.instance.EndPlayerTurn(playerName);
        }
        protected override void HandleOnAreaClick(string clickedCountryName) {
        }

        protected override void HandleOnAreaEnter(string countryName) {
        }

        protected override void HandleOnEscPress() {
        }
    }
}