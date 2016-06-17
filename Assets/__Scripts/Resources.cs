using UnityEngine;
using System.Collections;

public class Resources : MonoBehaviour {

    /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    public static Resources S;

    

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void UpdateResources(ResultDefinition r) {
        //update resources
        Player.S.wellbeing += r.resultWellbeing;
        Player.S.language += r.resultLanguage;
        Player.S.gpa += r.resultGPA;

        //min and max the resources
        if (Player.S.wellbeing > 100) { Player.S.wellbeing = 100; }
        else if (Player.S.wellbeing < 0) { Player.S.wellbeing = 0; }

        if (Player.S.language > 50) { Player.S.language = 50; } 
        else if (Player.S.language < 0) { Player.S.language = 0; }

        if (Player.S.gpa > 4f) { Player.S.gpa = 4.0f; } 
        else if (Player.S.gpa < 0f) { Player.S.gpa = 0.0f; }

        //update GUI
        GUI.S.UpdateGUI(Player.S.wellbeing, Player.S.language, Player.S.gpa, r.resultWellbeing, r.resultLanguage,r.resultGPA);
    }

    //check wellbeing level

    //check gpa and language level

}
