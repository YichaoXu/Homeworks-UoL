namespace RiskSrc.Controller.Command.Unique {
    public abstract class AbstractRiskExclusiveCommand: IRiskCommand {
        
        public abstract string Description { get; }

        public bool Execute() {
            return ExecuteCore();
        }

        public bool Rollback() {
            return UndoCore();
        }

        protected abstract bool ExecuteCore();
        protected abstract bool UndoCore();
    }
}