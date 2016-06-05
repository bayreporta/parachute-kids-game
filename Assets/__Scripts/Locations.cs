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
    public float actOneChallenge = 0;
    public float actTwoChallenge = 0;
    public float actThreeChallenge = 0;
    public float xLoc = 0;
    public float yLoc = 0;
    public float zLoc = 0;
}

public class Locations : MonoBehaviour {


    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static Locations S;
    public int totLocations = 10;
    public List<GameObject> locationObjects;
    public JsonData locationData;
    public GameObject locationPrefab;
    public List<LocationDefinition> locationDefinitions;
    public List<LocationType> locTypes;
    public bool blockLocationClick = false;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void CreateLocations() {
        locationObjects = new List<GameObject>();

        // convert json and parse locations into GameObjects
        locationData = Utils.ConvertJson("/_Resources/locations.json");
        GameObject locationParent = GameObject.Find("_Locations");

        for (int i=0; i < totLocations; i++) {
            GameObject locGO = Instantiate(locationPrefab) as GameObject;         

            Vector3 pos;
            pos.x = float.Parse(locationData[0][i]["x"].ToString());
            pos.y = float.Parse(locationData[0][i]["y"].ToString());
            pos.z = 0;
            locGO.transform.position = pos;

            Vector3 scale;
            scale.x = 3;
            scale.y = 3;
            scale.z = 1;
            locGO.transform.localScale = scale;

            locGO.name = locationData[0][i]["key"].ToString();
            locGO.tag = "Locations";
            locGO.transform.parent = locationParent.transform;
            locGO.AddComponent<Locations>();

            locationObjects.Add(locGO);
        }
    }

    public void GetLocationDefinitions() {
        locationDefinitions = new List<LocationDefinition>();
        locTypes = new List<LocationType>();
        locationData = Utils.ConvertJson("/_Resources/locations.json");

        //grab all values from LocationType enum
        foreach (LocationType l in System.Enum.GetValues(typeof(LocationType))) {
            locTypes.Add(l);
        }

        //build locations data from json
        for (int i = 0; i < Locations.S.totLocations; i++) {
            LocationDefinition loc = new LocationDefinition();

            loc.type = locTypes[i];
            loc.name = locationData[0][i]["name"].ToString();
            //loc.description = locationData[0][i]["description"].ToString();
            loc.actOne = Convert.ToBoolean(locationData[0][i]["actone"].ToString());
            loc.actTwo = Convert.ToBoolean(locationData[0][i]["acttwo"].ToString());
            loc.actThree = Convert.ToBoolean(locationData[0][i]["actthree"].ToString());
            //loc.actOneChallenge = float.Parse(locationData[0][i]["actonechallenge"].ToString());
            //loc.actTwoChallenge = float.Parse(locationData[0][i]["acttwochallenge"].ToString());
            //loc.actThreeChallenge = float.Parse(locationData[0][i]["actthreechallenge"].ToString());
            loc.xLoc = float.Parse(locationData[0][i]["x"].ToString());
            loc.yLoc = float.Parse(locationData[0][i]["y"].ToString());
            loc.zLoc = 0;

            locationDefinitions.Add(loc);
        }
    }

    void OnMouseUp() {
        //which location did player click
        string locationPicked = this.gameObject.name;

        //lets grab the challenge
        if (!blockLocationClick) Challenges.S.RetrieveChallenge(Player.S.currCharacter, Player.S.currAct, locationPicked);

    }

}
