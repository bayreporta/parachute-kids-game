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
    public int locationID;
    public string locationType;
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

public class LocationControl : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    public static LocationControl S;
    public GameObject locationPrefab;
    public int totLocations = 10;
    public JsonData locationData;
    public TextAsset locationJson;
    public List<LocationDefinition> locationDefinitions;
    public List<LocationType> locTypes;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void GetLocationDefinitions() {
        locationData = new JsonData();

        locationDefinitions = new List<LocationDefinition>();
        locTypes = new List<LocationType>();
        locationData = Utils.S.ConvertJson(locationJson);

        //grab all values from LocationType enum
        foreach (LocationType l in System.Enum.GetValues(typeof(LocationType))) {
            locTypes.Add(l);
        }

        //build locations data from json
        for (int i = 0; i < totLocations; i++) {
            LocationDefinition loc = new LocationDefinition();

            loc.type = locTypes[i];
            loc.locationID = int.Parse(locationData[0][i]["id"].ToString());
            loc.name = locationData[0][i]["name"].ToString();
            loc.locationType = locationData[0][i]["locationtype"].ToString();
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

    public void CreateLocations() {
        GameObject locationParent = GameObject.Find("_Locations");

        //create locations
        for (int i=0; i < LocationControl.S.totLocations; i++) {
            Instantiate(locationPrefab);
        }

        //configure each location
        for (int i = 0; i < Locations.locationObjects.Count; i++) {
            LocationDefinition loc = ParachuteKids.S.GetLocationDefinition((LocationType)i);
            Locations go = Locations.locationObjects[i];

            //position on gamespace
            Vector3 pos;
            pos.x = loc.xLoc;
            pos.y = loc.yLoc;
            pos.z = 0;
            go.transform.position = pos;

            //scale of go
            Vector3 scale;
            scale.x = 3;
            scale.y = 3;
            scale.z = 1;
            go.transform.localScale = scale;

            go.name = loc.name;
            go.type = loc.type;
            go.tag = "Locations";
            go.locationType = loc.locationType;
            go.transform.parent = locationParent.transform;
        }
    }

    public void ActivateLocation() {
        if (Player.S.wellbeing <= 30) {
            ChallengeDefinition chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)10);

            if (chal.clickedFlag == false) {
                Locations go = Locations.locationObjects[9];
                go.clickableLocation = true;
				Acts.S.challengeThisAct += 1;
                go.GetComponent<Renderer>().material.color = Color.green;
            }
            
        }

        if (Player.S.language >= 40) {
            ChallengeDefinition chal = ParachuteKids.S.GetChallengeDefinition((ChallengeType)11);

            if (chal.clickedFlag == false) {
                Locations go = Locations.locationObjects[1];
                go.clickableLocation = true;
				Acts.S.challengeThisAct += 1;
                go.GetComponent<Renderer>().material.color = Color.green;
            }

        }
    }

}
