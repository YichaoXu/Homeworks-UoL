using UnityEngine;
using WPMF;

public class MapEvent : MonoBehaviour {
    
    public static MapEvent instance;

    public delegate void keyboardEventDelegate();

    public delegate void mapInteractEventDelegate(string areaName);

    public event mapInteractEventDelegate OnAreaClickEvent;
    public event mapInteractEventDelegate OnAreaEnterEvent;
    public event keyboardEventDelegate OnEscPressEvent;


    private void Awake() {
        instance = this;
        var map = WorldMap2D.instance;
        map.OnCountryClick += ClickOnArea;
        map.OnCountryEnter += MoveOnArea;
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) OnEscPressEvent?.Invoke();
    }

    private void ClickOnArea(int areaIndex, int regionIndex) {
        var areaName = WorldMap2D.instance.countries[areaIndex].name;
        OnAreaClickEvent?.Invoke(areaName);
    }

    private void MoveOnArea(int areaIndex, int regionIndex) {
        var areaName = WorldMap2D.instance.countries[areaIndex].name;
        OnAreaEnterEvent?.Invoke(areaName);
    }
}