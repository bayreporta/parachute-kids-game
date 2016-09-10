using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Acts : MonoBehaviour {

   /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    static public Acts S;
    public int challengesDoneForAct = 0;
    public int challengeThisAct = 0;
    public bool busStopChal = true;
    public bool apChal = true;

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

        //hide all locations
        for (int i = 0; i < LocationControl.S.locationObjects.Count; i++) {
            LocationControl.S.locationObjects[i].SetActive(false);
        }

        //cycle through challenges and activate location if applicable
        for (int i=0; i < Challenges.S.totChallenges; i++) {
            ChallengeDefinition chal = new ChallengeDefinition();
            chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)i);

            if (chal.actFlag == act) {
                GameObject go = LocationControl.S.locationObjects[chal.locationFlag];

                //activate location
                go.SetActive(true);

                //location highlight
                go.GetComponent<Animator>().SetBool("active", true);

                //location activate
                go.GetComponent<Locations>().clickableLocation = true;

                //add to total active challenges for act
                challengeThisAct += 1;


                //Act 3 special
                if (Player.S.currAct == 3) {
					//adjust gamespace and variables based on whether certain challenges are omitted off the bat
                    if (Player.S.wellbeing > 30 && chal.challengeID == 10) {
                        busStopChal = false;
                        go.SetActive(false);
                        challengeThisAct -= 1;
                    }             
                    else if (chal.challengeID == 11 && Player.S.language < 40) {
                        apChal = false;
                        go.SetActive(false);
                        challengeThisAct -= 1;
                    }       
                }             
            }
            
        }

        if (Player.S.wellbeing != 0) {
            if (Player.S.currAct == 2 || Player.S.currAct == 3) {
                GeneralCanvas.S.UpdateActCanvas(Player.S.currAct); //update canvas text
                GeneralCanvas.S.generalCanvas.SetActive(true);
                StartCoroutine(GeneralCanvas.S.TransitionActCanvas(1)); //transition canvas in
            } else if (Player.S.currAct == 4) {
                CollegeCanvas.S.collegeCanvas.SetActive(true);
                GUIControl.S.GUICanvas.SetActive(false);
                ArtAssets.S.ControlBackground(1);
                StartCoroutine(CollegeCanvas.S.TransitionCollegeCanvas(1)); //transition canvas in            
            }

            //start Act Canvas transition
            Invoke("FireActTransition", 3f);
        }     
    }

    public void ReevaluateActThree() {
        //AP Check
        if (Player.S.language >= 40 && LocationControl.S.locationObjects[1].gameObject.GetComponent<Locations>().clickableLocation == true && apChal == false) {
            LocationControl.S.locationObjects[1].gameObject.SetActive(true);
            LocationControl.S.locationObjects[1].GetComponent<Animator>().SetBool("active", true);
            challengeThisAct += 1;
            apChal = true;
        }
        else if (Player.S.language < 40 && LocationControl.S.locationObjects[1].gameObject.GetComponent<Locations>().clickableLocation == true && apChal == true) {
            LocationControl.S.locationObjects[1].gameObject.SetActive(false);
            challengeThisAct -= 1;
            apChal = false;
        }

        //Bus Stop Check
        if (Player.S.wellbeing <= 30 && LocationControl.S.locationObjects[9].gameObject.GetComponent<Locations>().clickableLocation == true && busStopChal == false) {
            LocationControl.S.locationObjects[9].gameObject.SetActive(true);
            LocationControl.S.locationObjects[9].GetComponent<Animator>().SetBool("active", true);
            challengeThisAct += 1;
            busStopChal = true;            
        } else if (Player.S.wellbeing > 30 && LocationControl.S.locationObjects[9].gameObject.GetComponent<Locations>().clickableLocation == true && busStopChal == true) {
            LocationControl.S.locationObjects[9].gameObject.SetActive(false);
            challengeThisAct -= 1;
            busStopChal = false;
        }
    }

    public void FireActTransition() {
        StartCoroutine(GeneralCanvas.S.TransitionActCanvas(0));
    }
}
