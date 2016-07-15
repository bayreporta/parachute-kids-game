using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Locations : MonoBehaviour {    

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public string locationType;
    public bool clickableLocation; 

    /* FUNCTIONS
    ---------------------------------------------------------------*/

    void OnMouseUp() {
        //lets grab the challenge
        if (clickableLocation) {
            Challenges.S.RetrieveChallenge(Player.S.currCharacter, Player.S.currAct, locationType);

            //restore default location
            clickableLocation = false;
            Animator ani = GetComponent<Animator>();
            ani.SetBool("active", false);
        }
    }

}
