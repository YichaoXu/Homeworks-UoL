using WPMF;
using UnityEngine;
using UnityEngine.UI;
using Risk.Controller.Map;
using Risk.Controller.UIComponent;


public class RiskViewControllerDelegate : MonoBehaviour {

    private WorldMap2D mapView;
    [SerializeField] private Text turnText;
    [SerializeField] private Text phaseText;
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private GameObject settingCanvas;

    private AbstractRiskMapController mapController;
    private UIComponentControllerInterface componentController;

    // The initiation method in Unity GameObject
    void Start() {
        mapView = WorldMap2D.instance;
        mapController = new MapIgnoreController(mapView);
        componentController = new RiskUIComponentController {
            gameTurnTitle = turnText,
            gamePhaseTitle = phaseText,
            gameResultPanel = resultCanvas,
            gameSettingPanel = settingCanvas
        };

        mapView.OnCountryClick += ClickOnCountry;
        mapView.OnCountryEnter += MoveOnCountry;
        mapView.OnCountryExit += MoveOutCountry;
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape))
            mapController.HandleEscapeKeyPress();
    }

    public void ClickNextTurnButton() {
        mapController.MoveToNextTurn();
        componentController.UpdateTurnText();
        componentController.UpdatePhaseText("Initiation");
        mapController = new MapIgnoreController(mapController);
    }

    public void ClickAttackButton() {
        mapController = new MapAttackController(mapController);
        componentController.UpdatePhaseText("Attack");
    }

    public void ClickRecruitButton() {
        mapController = new MapRecruitController(mapController);
        componentController.UpdatePhaseText("Recruit");
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

    private void ClickOnCountry(int countryIndex, int regionIndex) {
        var countryName = mapView.countries[countryIndex].name;
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
        Debug.Log("Press");
        mapController.HandleEscapeKeyPress();
    }
}