using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Acts : MonoBehaviour {

   /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    static public Acts S;
    public int[] challengesPerAct;
    public int[] challengesDoneForAct;

    //private----------------------------//


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;

        //configure arrays
        challengesPerAct = new int[3];
        challengesPerAct[0] = 0;
        challengesPerAct[1] = 0;
        challengesPerAct[2] = 0;

        challengesDoneForAct = new int[3];
        challengesDoneForAct[0] = 0;
        challengesDoneForAct[1] = 0;
        challengesDoneForAct[2] = 0;
    }

    public void CountChallengesPerAct(ChallengeDefinition chal) {
        switch (chal.actFlag) {
            case 1:
                challengesPerAct[0] += 1;
                break;
            case 2:
                challengesPerAct[1] += 1;
                break;
            case 3:
                challengesPerAct[2] += 1;
                break;
        }        
    }

    public void InitializeAct(int act) {
        Color activeLocation = Color.green;

        for (int i=0; i < Challenges.S.totChallenges; i++) {
            ChallengeDefinition chal = new ChallengeDefinition();
            chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)i);

            if (chal.actFlag == act) {
                //grab the location definition
                Locations.locationObjects[chal.locationFlag].GetComponent<Renderer>().material.color = activeLocation;

            }

            //Debug.Log(chal.type);
        }

        
    }
}
