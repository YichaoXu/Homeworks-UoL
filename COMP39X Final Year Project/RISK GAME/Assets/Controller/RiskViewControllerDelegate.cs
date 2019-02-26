using WPMF;
using UnityEngine;
using UnityEngine.UI;
using Risk.Controller.Map;
using Risk.Controller.UIComponent;


public class RiskViewControllerDelegate : MonoBehaviour {

    private WorldMap2D mapView;
    [SerializeField] private Text turnText;
    [SerializeField] private Text phaseText;
    [SerializeField] private Text deployingText;
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private GameObject settingCanvas;
    [SerializeField] private GameObject deployingCanvas;
    
    private AbstractRiskMapController mapController;
    private AbstractUIComponentController componentController;

    // The initiation method in Unity GameObject
    void Start() {
        var uiComponents = new UnityUIComponents {
            gameTurnTitle = turnText,
            gamePhaseTitle = phaseText,
            deployingTitle = deployingText,
            gameResultPanel = resultCanvas,
            gameSettingPanel = settingCanvas,
            deployingPanel = deployingCanvas
        };
        mapView = WorldMap2D.instance;
        mapController = new MapInitiateController(mapView);
        componentController = new UIComponentInitiateController(uiComponents);

        mapView.OnCountryClick += ClickOnCountry;
        mapView.OnCountryEnter += MoveOnCountry;
        mapView.OnCountryExit += MoveOutCountry;
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape))
            mapController.HandleEscapeKeyPress();
    }

    public void ClickNextTurnButton() {
        mapController = new MapInitiateController(mapController);
        componentController = new UIComponentInitiateController(componentController);
    }

    public void ClickAttackButton() {
        mapController = new MapAttackController(mapController);
        componentController = new UIComponentAttackController(componentController);

    }

    public void ClickRecruitButton() {
        mapController = new MapRecruitController(mapController);
        componentController = new UIComponentRecruitController(componentController);
    }

    public void ClickExitButton() {
        componentController.HandleExitButtonClick();
    }

    public void ClickBackToMainButton() {
        componentController.HandleBackToMainButton();
    }

    public void ClickBackToDesktopButton() {
        componentController.HandleBackToDesktopButton();
    }

    public void ClickBackToGameButton() {
        componentController.HandleBackToGameButtonClick();
    }

    public void ClickSettingButton() {
        componentController.HandleSettingButtonClick();
    }

    public void SetScrollerBar(float number) {
        ((UIComponentRecruitController) componentController).updateArmiesSize(number);
    }

    public void ClickConfirmButton() {
        ((UIComponentRecruitController) componentController).HandleConfirmButtonClick();
        ((MapRecruitController)mapController).UpdateCountryArmies();
    }

    private void ClickOnCountry(int countryIndex, int regionIndex) {
        var countryName = mapView.countries[countryIndex].name;
        componentController.HandleOnCountryClick(countryName);
        mapController.HandleOnCountryClick(countryName);
    }

    private void MoveOnCountry(int countryIndex, int regionIndex) {
        var countryName = mapView.countries[countryIndex].name;
        mapController.HandleCursorInCountryMovement(countryName);
    }

    private void MoveOutCountry(int countryIndex, int regionIndex) {
        var countryName = mapView.countries[countryIndex].name;
        mapController.HandleCursorOutCountryMovement(countryName);
    }

    private void PressEscape() {
        mapController.HandleEscapeKeyPress();
    }
}