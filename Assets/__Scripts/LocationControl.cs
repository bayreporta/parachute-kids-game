using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class LocationControl : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static LocationControl S;
    public GameObject locationParent;
    public List<GameObject> locationObjects;
    public int totLocations = 10;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }
    /*
    public void ActivateLocation() {
        if (Player.S.wellbeing <= 30) {
            ChallengeDefinition chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)10);

			if (chal.clickedFlag == false && chal.allowedFlag == false) {
                GameObject go = locationObjects[9];

                Animator ani = go.GetComponent<Animator>();
                ani.SetBool("active", true);

                chal.allowedFlag = true;
                go.GetComponent<Locations>().clickableLocation = true;
				Acts.S.challengeThisAct += 1;
            }
        } else {
            ChallengeDefinition chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)10);

            if (chal.clickedFlag == false && chal.allowedFlag == true) {
                GameObject go = locationObjects[9];

                Animator ani = go.GetComponent<Animator>();
                ani.SetBool("active", false);

                chal.allowedFlag = false;
                go.GetComponent<Locations>().clickableLocation = false;
                Acts.S.challengeThisAct -= 1;
            }

        }

        if (Player.S.language >= 40) {
            ChallengeDefinition chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)11);

            if (chal.clickedFlag == false) {
                GameObject go = locationObjects[1];

                Animator ani = go.GetComponent<Animator>();
                ani.SetBool("active", true);

                chal.allowedFlag = true;
                go.GetComponent<Locations>().clickableLocation = true;
				Acts.S.challengeThisAct += 1;
            }

        } else {
            ChallengeDefinition chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)11);

            if (chal.clickedFlag == false && chal.allowedFlag == true) {
                GameObject go = locationObjects[1];

                Animator ani = go.GetComponent<Animator>();
                ani.SetBool("active", false);

                chal.allowedFlag = false;
                go.GetComponent<Locations>().clickableLocation = false;
                Acts.S.challengeThisAct -= 1;
                
            }

        }
    }
    */
}
