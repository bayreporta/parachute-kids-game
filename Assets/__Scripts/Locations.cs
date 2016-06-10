using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Locations : MonoBehaviour {
    public static List<Locations> locationObjects;

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public LocationType type;
    public string locationType;
    public bool clickableLocation;
    Vector3 pos;
    Vector3 scale;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        if (locationObjects == null) {
            locationObjects = new List<Locations>();
        }
        locationObjects.Add(this);
    }    

    void OnMouseUp() {
        //lets grab the challenge
        if (clickableLocation) Challenges.S.RetrieveChallenge(Player.S.currCharacter, Player.S.currAct, locationType);

    }

}
