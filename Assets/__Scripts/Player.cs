using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    static public Player S;

    //current game variables----------------------------//
    public string currCharacter = "JohnDoe";
    public int currAct = 0;
    public int wellbeing = 50;
    public int language = 0;
    public float gpa = 2.0f;
    public int collegeChoice;


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

}
