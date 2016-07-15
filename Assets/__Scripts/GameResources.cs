using UnityEngine;
using System.Collections;
using SVGImporter;
using System;

public class GameResources : MonoBehaviour {

    /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    public static GameResources S;    

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

        if (Player.S.gpa > 5f) { Player.S.gpa = 5.0f; } 
        else if (Player.S.gpa < 0f) { Player.S.gpa = 0.0f; }

        //update GUI
        GUIControl.S.UpdateGUI(Player.S.wellbeing, Player.S.language, Player.S.gpa, r.resultWellbeing, r.resultLanguage,r.resultGPA);
    
		//wellbeing fail check
		if (Player.S.wellbeing == 0) {
			
			//activate Results panel
			GeneralCanvas.S.generalCanvas.SetActive(false);
			GeneralCanvas.S.generalActPanel.SetActive(false);	

			//fire game over
			EndGame.S.PopulateEnding(0);	
		}

        //GUI warning regarding wellbeing
        if (Player.S.wellbeing <= 30) {
            GUIControl.S.wellbeingWarning.gameObject.SetActive(true);
        } else {
            GUIControl.S.wellbeingWarning.gameObject.SetActive(false);
        }

    }
}
