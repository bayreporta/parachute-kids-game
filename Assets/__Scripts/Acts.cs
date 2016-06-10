using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Acts : MonoBehaviour {

   /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    static public Acts S;
    public int[] challengesPerAct;

    //private----------------------------//


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;

        //configure array
        challengesPerAct = new int[3];
        challengesPerAct[0] = 0;
        challengesPerAct[1] = 0;
        challengesPerAct[2] = 0;
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

    public void InitializeAct() {

    }
}
