using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static GUI S;
    public GameObject GUIControl;
    public GUIText wellbeingGUI;
    public GUIText languageGUI;
    public GUIText gpaGUI;
    public GUIText actGUI;

    GameObject wellbeingGO;
    GameObject gpaGO;
    GameObject languageGO;
    GameObject actGO;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void InitGUI(string chara) {
        GUIControl = GameObject.Find("_GUI");
        wellbeingGO = GameObject.Find("GUI_Wellbeing");
        gpaGO = GameObject.Find("GUI_GPA");
        languageGO = GameObject.Find("GUI_Language");
        actGO = GameObject.Find("GUI_Act");
        wellbeingGUI = wellbeingGO.GetComponent<GUIText>();
        languageGUI = languageGO.GetComponent<GUIText>();
        gpaGUI = gpaGO.GetComponent<GUIText>();
        actGUI = actGO.GetComponent<GUIText>();

        /*init GUI based on character
        switch (chara) {
            case "JohnDoe":
                wellbeingGUI.text = "Wellbeing " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingWellbeing.ToString();
                languageGUI.text = "Language " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingLanguage.ToString();
                gpaGUI.text = "GPA " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingGPA.ToString() + ".00";
                Player.S.wellbeing = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingWellbeing;
                Player.S.language = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingLanguage;
                Player.S.gpa = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingGPA;
                break;
        }*/

        //initialize GUI text
        wellbeingGUI.text = "Wellbeing: " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingWellbeing.ToString();
        languageGUI.text = "Language: Test Pending";
        gpaGUI.text = "GPA: " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingGPA.ToString() + ".00";

    }

    public void UpdateGUI(int wb, int lang, float gpa, int wbR, int langR, float gpaR) {
        //update GUI colors based on whether they went up or down in value
        ChangeGUIColor(wellbeingGUI, wbR);
        ChangeGUIColor(languageGUI, langR);
        ChangeGUIColor(gpaGUI, gpaR);

        wellbeingGUI.text = "Wellbeing: " + wb.ToString();

        //convert language number into text indicator
        languageGUI.text = "Language: " + ConvertLanguageValue(lang);

        //Convert gpa value
        gpaGUI.text = "GPA: " + ConvertGPAValue(gpa);
    }

    public void ChangeGUIColor(GUIText gui, float i) {
        //reset GUI up/down indicators
        gui.color = Color.white;

        //figure out which color
        if (i < 0) {gui.color = Color.red;} 
        else if (i > 0) {gui.color = Color.green;}
        
    }

    public void ChangeActGUI(int act) {
        if (act == 0) { act = 1; }
        else if (act == 4) { act = 3; }

        actGUI.text = "Act " + act.ToString();
    }

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
