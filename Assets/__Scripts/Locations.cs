using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public enum LocationType {
    Classroom,
    Counselor,
    EdCenter,
    Cafeteria,
    Home,
    TeaHouse,
    Phone,
    Stadium,
    KaraokeBar,
    BusStop
}

public class LocationDefinition {
    public LocationType type;
    public string description;
    public string name;
    public bool actOne = false;
    public bool actTwo = false;
    public bool actThree = false;
    public int actOneChallenge = 0;
    public int actTwoChallenge = 0;
    public int actThreeChallenge = 0;
    public float xLoc = 0;
    public float yLoc = 0;
    public float zLoc = 0;
}

public class Locations : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    //dynamic
    static public int totLocations = 10;
    static public List<GameObject> locationObjects;
    static public JsonData locationData;

    public GameObject locationPrefab;

    public static List<LocationDefinition> locationDefinitions;
    public static List<LocationType> locTypes;

    //private


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    public static void CreateLocations() {
        locationObjects = new List<GameObject>();

        // convert json and parse locations into GameObjects
        locationData = Utils.ConvertJson("/_Resources/locations.json");
        GameObject locationParent = GameObject.Find("_Locations");

        for (int i=0; i < totLocations; i++) {
            var go = new GameObject();            

            Vector3 pos;
            pos.x = float.Parse(locationData[0][i]["x"].ToString());
            pos.y = float.Parse(locationData[0][i]["y"].ToString());
            pos.z = 0;
            go.transform.position = pos;

            Vector3 scale;
            scale.x = 3;
            scale.y = 3;
            scale.z = 1;
            go.transform.localScale = scale;

            go.gameObject.AddComponent<MeshRenderer>();
            go.gameObject.AddComponent<MeshFilter>();
            go.gameObject.GetComponent<Renderer>().material.color = Color.white;

            go.name = locationData[0][i]["key"].ToString();
            go.tag = "Locations";
            go.transform.parent = locationParent.transform;
            go.AddComponent<Locations>();

            locationObjects.Add(go);
        }
    }

    public static void GetLocationDefinitions() {
        locationDefinitions = new List<LocationDefinition>();
        locTypes = new List<LocationType>();
        locationData = Utils.ConvertJson("/_Resources/locations.json");

        //grab all values from LocationType enum
        foreach (LocationType l in System.Enum.GetValues(typeof(LocationType))) {
            locTypes.Add(l);
        }

        //build locations data from json
        for (int i = 0; i < Locations.totLocations; i++) {
            LocationDefinition loc = new LocationDefinition();

            loc.type = locTypes[i];
            loc.name = locationData[0][i]["name"].ToString();
            //loc.description = locationData[0][i]["description"].ToString();
            //loc.actOne = Convert.ToBoolean(locationData[0][i]["actOne"].ToString());
            //loc.actTwo = Convert.ToBoolean(locationData[0][i]["actTwo"].ToString());
            //loc.actThree = Convert.ToBoolean(locationData[0][i]["actThree"].ToString());
            //loc.actOneChallenge = Convert.ToInt32(locationData[0][i]["actOneChallenge"].ToString());
            //loc.actTwoChallenge = Convert.ToInt32(locationData[0][i]["actTwoChallenge"].ToString());
            //loc.actThreeChallenge = Convert.ToInt32(locationData[0][i]["actThreeChallenge"].ToString());
            //loc.xLoc = Convert.ToInt32(locationData[0][i]["x"].ToString());
            //loc.yLoc = Convert.ToInt32(locationData[0][i]["y"].ToString());
            loc.zLoc = 0;

            locationDefinitions.Add(loc);
        }
    }
}
