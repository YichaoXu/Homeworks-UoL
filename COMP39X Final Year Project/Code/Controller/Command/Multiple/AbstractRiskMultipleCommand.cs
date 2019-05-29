namespace RiskSrc.Controller.Command.Multiple {
    public abstract class AbstractRiskMultipleCommand : IRiskCommand {

        private bool isCommitted;
        private bool isModelUpdated;
        private bool isViewUpdated;
        public abstract string Description { get; }

        protected AbstractRiskMultipleCommand() {
            isCommitted = false;
            isModelUpdated = false;
            isViewUpdated = false;
        }

        public bool Start() {
            if (isCommitted || isViewUpdated)
                return false;
            isViewUpdated = ViewUpdateCore();
            return isViewUpdated;
        }
        public bool Execute() {
            if (isCommitted || !isViewUpdated || isModelUpdated)
                return false;
            isModelUpdated = ModelUpdateCore();
            return isModelUpdated;
        }

        public bool Rollback() {
            if (isCommitted)
                return false;
            
            var isRollback = true;
            if (isModelUpdated) {
                isRollback &= ModelUpdateRollbackCore();
                isModelUpdated = false;
            }
            if (isViewUpdated) {
                isRollback &= ViewUpdateRollbackCore();
                isViewUpdated = false;
            }
            return isRollback;
        }

        public bool Commit() {
            if (isCommitted)
                return false;
            if (isViewUpdated && isModelUpdated)
                isCommitted = true;
            return isCommitted;
        }


        protected abstract bool ViewUpdateCore();
        protected abstract bool ModelUpdateCore();
        protected abstract bool ViewUpdateRollbackCore();
        protected abstract bool ModelUpdateRollbackCore();
    }
}