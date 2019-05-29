namespace RiskSrc.Controller.Command {
    interface IRiskCommand {
        bool Execute();
        bool Rollback();
    }  
}