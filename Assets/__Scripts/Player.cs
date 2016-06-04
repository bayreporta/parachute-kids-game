using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    static public Player S;

    //current game variables----------------------------//
    public string currCharacter = "JohnDoe";
    public int currAct = 1;


    //private----------------------------//


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

}
