namespace RiskSrc.Controller.HumanPlayer.Phase {
    public abstract class AbstractHumanPlayerPhaseControllerCore {

        protected readonly string playerName;
        protected AbstractHumanPlayerPhaseControllerCore(string playerName) {
            this.playerName = playerName;
        }
        public void Work() {
            MapEvent.instance.OnAreaClickEvent += HandleOnAreaClick;
            MapEvent.instance.OnAreaEnterEvent += HandleOnAreaEnter;
            MapEvent.instance.OnEscPressEvent += HandleOnEscPress;
            WorkCore();
        }
        public void Stop() {
            MapEvent.instance.OnAreaClickEvent -= HandleOnAreaClick;
            MapEvent.instance.OnAreaEnterEvent -= HandleOnAreaEnter;
            MapEvent.instance.OnEscPressEvent -= HandleOnEscPress;
            StopCore();
        }

        protected virtual void WorkCore() {}
        protected virtual void StopCore() {}
        
        protected abstract void HandleOnAreaClick(string clickedCountryName);
        protected abstract void HandleOnAreaEnter(string countryName);
        protected abstract void HandleOnEscPress();

    }
}