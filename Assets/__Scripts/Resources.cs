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
        Player.S.wellbeing += r.resultWellbeing;
        Player.S.language += r.resultLanguage;
        Player.S.gpa += r.resultGPA;

        GUI.S.UpdateGUI(Player.S.wellbeing, Player.S.language, Player.S.gpa, r.resultWellbeing, r.resultLanguage,r.resultGPA);
    }

    //check wellbeing level

    //check gpa and language level

}
