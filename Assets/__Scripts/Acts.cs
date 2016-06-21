using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Acts : MonoBehaviour {

   /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    static public Acts S;
    public int challengesDoneForAct = 0;
    public int challengeThisAct = 0;

    //private----------------------------//


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void CheckActStatus() {
        //this is the end of game check based on what Act it is atm
        if (challengeThisAct == challengesDoneForAct && challengesDoneForAct != 0) {
            //code to move to next act
            Player.S.currAct += 1;
            if (Player.S.currAct > 4) {
                challengesDoneForAct = 0;
                challengeThisAct = 0;
                //end game code
            }
            else { InitializeAct(Player.S.currAct); }            
        }
    }

 
    public void InitializeAct(int act) {
        Color activeLocation = Color.green;
        challengesDoneForAct = 0;
        challengeThisAct = 0;

        for (int i=0; i < Challenges.S.totChallenges; i++) {
            ChallengeDefinition chal = new ChallengeDefinition();
            chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)i);

            if (chal.actFlag == act) {
                Locations go = Locations.locationObjects[chal.locationFlag];

                //in here we need to check prereqs and determine which challenges appear and which do not

                //update GUI
                GUI.S.ChangeActGUI(act);                   

                //location highlight
                go.GetComponent<Renderer>().material.color = activeLocation;

                //location activate
                go.clickableLocation = true;

                //add to total active challenges for act
                challengeThisAct += 1;

                //check if wellbeing is low enough for run away
                if (Player.S.currAct == 3) {
                    if (Player.S.wellbeing > 30 && chal.challengeID == 10 || chal.challengeID == 11 && Player.S.language >= 40) {
                        go.clickableLocation = false;
                        go.GetComponent<Renderer>().material.color = Color.white;
                    }                    
                }
            }
        }

       
       if (Player.S.currAct == 2 || Player.S.currAct == 3) {
            GeneralCanvas.S.UpdateActCanvas(Player.S.currAct); //update canvas text
            GeneralCanvas.S.generalCanvas.SetActive(true);
            StartCoroutine(GeneralCanvas.S.TransitionActCanvas(1)); //transition canvas in
       }
       else if (Player.S.currAct == 4) {
            CollegeCanvas.S.collegeCanvas.SetActive(true);
            StartCoroutine(CollegeCanvas.S.TransitionCollegeCanvas(1)); //transition canvas in
            
       }

       //start Act Canvas transition
       Invoke("FireActTransition", 3f);
    }

    public void FireActTransition() {
        StartCoroutine(GeneralCanvas.S.TransitionActCanvas(0));
    }
}
