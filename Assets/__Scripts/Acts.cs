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
                GameObject go = LocationControl.S.locationObjects[chal.locationFlag];

                //update GUI
                //GUIControl.S.ChangeActGUI(act);                   

                //location highlight
                go.GetComponent<Animator>().SetBool("active", true);

                //location activate
                go.GetComponent<Locations>().clickableLocation = true;

                //add to total active challenges for act
                challengeThisAct += 1;


                //Act 3 special
                /*if (Player.S.currAct == 3) {
					//adjust gamespace and variables based on whether certain challenges are omitted off the bat
                    if (Player.S.wellbeing > 30 && chal.challengeID == 10 || chal.challengeID == 11 && Player.S.language < 40) {
                        //disable challenge
                        if (chal.challengeID == 10) chal.allowedFlag = false;
                        if (chal.challengeID == 11) chal.allowedFlag = false;

                        go.GetComponent<Locations>().clickableLocation = false;
						challengeThisAct -= 1;
                        go.GetComponent<Animator>().SetBool("active", false);
                    }                    
                }*/
            }
        }

       
       if (Player.S.currAct == 2 || Player.S.currAct == 3) {
            GeneralCanvas.S.UpdateActCanvas(Player.S.currAct); //update canvas text
            GeneralCanvas.S.generalCanvas.SetActive(true);
            StartCoroutine(GeneralCanvas.S.TransitionActCanvas(1)); //transition canvas in
       }
       else if (Player.S.currAct == 4) {
            CollegeCanvas.S.collegeCanvas.SetActive(true);
            GUIControl.S.GUICanvas.SetActive(false);
            ArtAssets.S.ControlBackground(1);
            StartCoroutine(CollegeCanvas.S.TransitionCollegeCanvas(1)); //transition canvas in            
       }

       //start Act Canvas transition
       Invoke("FireActTransition", 3f);
    }

    public void FireActTransition() {
        StartCoroutine(GeneralCanvas.S.TransitionActCanvas(0));
    }
}
