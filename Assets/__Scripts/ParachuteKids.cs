using UnityEngine;
using System.Collections;
using LitJson;

public class ParachuteKids : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    static public ParachuteKids S;

    //dynamic


    //private
    private JsonData data;

	/* UNITY FUNCTIONS
    ---------------------------------------------------------------*/
	void Awake () {
        S = this;

        //init game
        Locations.CreateLocations();   
    }


    /* CUSTOM FUNCTIONS
    ---------------------------------------------------------------*/
}
