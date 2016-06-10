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

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void InitGUI(string chara) {
        GameObject wellbeingGO = GameObject.Find("GUI_Wellbeing");
        GameObject gpaGO = GameObject.Find("GUI_GPA");
        GameObject languageGO = GameObject.Find("GUI_Language");
        GameObject actGO = GameObject.Find("GUI_Act");
        wellbeingGUI = wellbeingGO.GetComponent<GUIText>();
        languageGUI = languageGO.GetComponent<GUIText>();
        gpaGUI = gpaGO.GetComponent<GUIText>();
        actGUI = actGO.GetComponent<GUIText>();

        //init GUI based on character
        switch (chara) {
            case "JohnDoe":
                wellbeingGUI.text = "Wellbeing " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingWellbeing.ToString();
                languageGUI.text = "Language " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingLanguage.ToString();
                gpaGUI.text = "GPA " + ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingGPA.ToString();
                Player.S.wellbeing = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingWellbeing;
                Player.S.language = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingLanguage;
                Player.S.gpa = ParachuteKids.S.GetCharacterDefinition(CharacterType.JohnDoe).startingGPA;
                break;
        }
    }

    //update GUI function

}
