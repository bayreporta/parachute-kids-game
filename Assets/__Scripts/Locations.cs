using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class Locations : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    //dynamic
    static public GameObject[] locations;
    static public JsonData locationData;

    //private


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    static public void CreateLocations() {

        // convert json and parse locations into GameObjects
        int totLocations = 10;
        locationData = Utils.ConvertJson("/_Resources/locations.json");
        GameObject locationParent = GameObject.Find("_Locations");

        for (int i=0; i < totLocations; i++) {
           var go = new GameObject();
            Vector3 pos;
            pos.x = float.Parse(locationData[0][i]["x"].ToString());
            pos.y = float.Parse(locationData[0][i]["y"].ToString());
            pos.z = 0;
            go.transform.position = pos;

            go.name = locationData[0][i]["name"].ToString();
            go.tag = "Locations";
            go.transform.parent = locationParent.transform;
            go.AddComponent<Locations>();           
      
        }
    }
}
