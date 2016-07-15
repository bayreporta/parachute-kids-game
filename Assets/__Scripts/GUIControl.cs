using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIControl : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static GUIControl S;

    //GUI Elements
    public GameObject GUICanvas;
    public Text wellbeingVal;
    public Text languageVal;
    public Text gpaVal;
    public GameObject wellbeingWarning;

    //buttons top right
    public Button tutorialButton;
    public Button restartButton;
    public Button skiper;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void InitGUI() {
        GUICanvas.SetActive(true);

        // configure main vals
        wellbeingVal.text = "50%";
        languageVal.text = "Pending";
        gpaVal.text = "2.0";
        wellbeingWarning.SetActive(false);

        //colors
        ChangeGUIColor(wellbeingVal, 0);
        ChangeGUIColor(languageVal, 0);
        ChangeGUIColor(gpaVal, 0);

        // configure buttons
        tutorialButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        skiper.onClick.RemoveAllListeners();

        tutorialButton.onClick.AddListener(delegate { StartCoroutine(Tutorial.S.TransitionToTutorial(1)); });
        restartButton.onClick.AddListener(ParachuteKids.S.StartGame);

        // Skip ahead to End Game
        skiper.onClick.AddListener(delegate {
            Player.S.currAct = 4;
            Acts.S.InitializeAct(Player.S.currAct);
        });
    }
    
    public void UpdateGUI(int wb, int lang, float gpa, int wbR, int langR, float gpaR) {
        //update GUI colors based on whether they went up or down in value
        ChangeGUIColor(wellbeingVal, wbR);
        ChangeGUIColor(languageVal, langR);
        ChangeGUIColor(gpaVal, gpaR);

        wellbeingVal.text = wb.ToString();

        //convert language number into text indicator
        languageVal.text = ConvertLanguageValue(lang);

        //Convert gpa value
        gpaVal.text = ConvertGPAValue(gpa);
    }

    public void ChangeGUIColor(Text gui, float i) {
        //reset GUI up/down indicators
        gui.color = Color.white;

        //figure out which color
        if (i < 0) {gui.color = Color.red;} 
        else if (i > 0) {gui.color = Color.green;}        
    }

    /*public void ChangeActGUI(int act) {
        if (act == 0) { act = 1; }
        else if (act == 4) { act = 3; }

        actGUI.text = "Act " + act.ToString();
    }*/

    public string ConvertLanguageValue(int val) {
        string ret = "";

        if (val < 10) { ret = "ESL Level 1"; }
        if (val < 20 && val >= 10) { ret = "ESL Level 2"; }
        if (val < 30 && val >= 20) { ret = "ESL Level 3"; }
        if (val < 40 && val >= 30) { ret = "ESL Level 4"; }
        if (val < 50 && val >= 40) { ret = "ESL Level 5"; }
        if (val == 50 ) { ret = "Proficient"; }

        return ret;
    }

    public string ConvertGPAValue(float gpa) {
        string ret = "";
        
        if (gpa == 0) { ret = "0.0"; }
        else if (gpa == 1) { ret = "1.0"; }
        else if (gpa == 2) { ret = "2.0"; }
        else if (gpa == 3) { ret = "3.0"; }
        else if (gpa == 4) { ret = "4.0"; }
        else { ret = gpa.ToString(); }

        return ret;
    }
}
