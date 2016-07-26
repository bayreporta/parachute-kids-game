using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Locations : MonoBehaviour {    

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public string locationType;
    public bool clickableLocation;
    public string locationName;

    /* FUNCTIONS
    ---------------------------------------------------------------*/

    void OnMouseUp() {
        //lets grab the challenge
        if (clickableLocation) {
            Challenges.S.RetrieveChallenge(Player.S.currCharacter, Player.S.currAct, locationType);

            //restore default location
            clickableLocation = false;
            gameObject.SetActive(false);
            Animator ani = GetComponent<Animator>();
            ani.SetBool("active", false);
            LocationControl.S.locationCanvas.SetActive(false);

        }
    }

    void OnMouseEnter() {
        LocationControl.S.UpdateLocationCanvas(locationName);
    }

    void OnMouseExit() {
        LocationControl.S.locationCanvas.SetActive(false);
    }

}
