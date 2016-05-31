using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class ParachuteKids : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    static public ParachuteKids S;
    static public Dictionary<LocationType, LocationDefinition> LOC_DEFS;

    //dynamic

    //private
    private JsonData data;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake () {
        S = this;

        //init game
        Locations.S.CreateLocations();
        Locations.S.GetLocationDefinitions();

        //build location dictionary
        LOC_DEFS = new Dictionary<LocationType, LocationDefinition>();
        foreach (LocationDefinition loc in Locations.S.locationDefinitions) {
            LOC_DEFS[loc.type] = loc;
        }

        print(LOC_DEFS[LocationType.Classroom].xLoc);
    }

    

    
}
