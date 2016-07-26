using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class LocationControl : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static LocationControl S;
    public GameObject locationParent;
    public List<GameObject> locationObjects;
    public int totLocations = 10;

    //Canvas
    public GameObject locationCanvas;
    public Text locationText;


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
        
    }

    void Start() {
        locationCanvas.SetActive(false);
    }

    public void UpdateLocationCanvas(string loc) {
        locationCanvas.SetActive(true);
        locationText.text = loc;
    }  
}
