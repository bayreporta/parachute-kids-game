using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static GUI S;
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
        wellbeingGO = GameObject.Find("GUI_Wellbeing");
        gpaGO = GameObject.Find("GUI_GPA");
        languageGO = GameObject.Find("GUI_Language");
        actGO = GameObject.Find("GUI_Act");
        wellbeingGUI = wellbeingGO.GetComponent<GUIText>();
        languageGUI = languageGO.GetComponent<GUIText>();
        gpaGUI = gpaGO.GetComponent<GUIText>();
        actGUI = actGO.GetComponent<GUIText>();

        //init GUI based on character
        switch (chara) {
            case "JohnDoe":
                wellbeingGUI.text = "Wellbeing " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingWellbeing.ToString();
                languageGUI.text = "Language " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingLanguage.ToString();
                gpaGUI.text = "GPA " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingGPA.ToString() + ".00";
                Player.S.wellbeing = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingWellbeing;
                Player.S.language = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingLanguage;
                Player.S.gpa = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingGPA;
                break;
        }
    }

    public void UpdateGUI(int wb, int lang, float gpa, int wbR, int langR, float gpaR) {
        //update GUI colors based on whether they went up or down in value
        ChangeGUIColor(wellbeingGUI, wbR);
        ChangeGUIColor(languageGUI, langR);
        ChangeGUIColor(gpaGUI, gpaR);

        wellbeingGUI.text = "Wellbeing " + wb.ToString();
        gpaGUI.text = "GPA " + gpa.ToString();
        languageGUI.text = "Language " + lang.ToString();
    }

    public void ChangeGUIColor(GUIText gui, float i) {
        //reset GUI up/down indicators
        gui.color = Color.white;

        //figure out which color
        if (i < 0) {gui.color = Color.red;} 
        else if (i > 0) {gui.color = Color.green;}
        
    }
}
