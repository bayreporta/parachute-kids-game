using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public enum ChallengeType {
    JohnDoe_Act1_Classroom,
    JohnDoe_Act1_Counselor,
    JohnDoe_Act1_EdCenter,
    JohnDoe_Act1_Cafeteria,
    JohnDoe_Act1_Home,
    JohnDoe_Act2_EdCenter,
    JohnDoe_Act2_Cafeteria,
    JohnDoe_Act2_Home,
    JohnDoe_Act2_TeaHouse,
    JohnDoe_Act2_Phone,
    JohnDoe_Act3_Classroom,
    JohnDoe_Act3_Counselor,
    JohnDoe_Act3_Home,
    JohnDoe_Act3_Stadium,
    JohnDoe_Act3_KaraokeBar,
    JohnDoe_Act3_BusStop
}

public class ChallengeDefinition {
    public ChallengeType type;
    public int challengeID;    
    public string characterFlag;
    public int locationFlag;
    public int actFlag;
    public string title;
    public string flavorText;
    public string flavorImage;
    public string optionOneText;
    public string optionTwoText;
    public string optionOneResults;
    public string optionTwoResults;
    public int prereqChallenge;
    public string prereqResult;
    public string prereqWellbeing;
    public string prereqLanguage;
    public string prereqGPA;
    public bool clickedFlag = false;    
    public bool allowedFlag = false;

}

public class Challenges : MonoBehaviour {

    /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    static public Challenges S;
    public int totChallenges = 16;
    public JsonData challengeData;
    public List<ChallengeDefinition> challengeDefinitions;
    public List<ChallengeType> chalTypes;


    //private----------------------------//


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void GetChallengeDefinitions() {
        challengeDefinitions = new List<ChallengeDefinition>();
        chalTypes = new List<ChallengeType>();
        challengeData = Utils.ConvertJson("/_Resources/challenges.json");

        //grab all values from ChallengeType enum
        foreach (ChallengeType c in System.Enum.GetValues(typeof(ChallengeType))) {
            chalTypes.Add(c);            
        }

        //build challenge data from json
        for (int i = 0; i < Challenges.S.totChallenges; i++) {
            ChallengeDefinition chal = new ChallengeDefinition();

            chal.type = chalTypes[i];
            chal.prereqChallenge = int.Parse(challengeData[0][i]["prereqchallenge"].ToString()); 
            chal.prereqResult = challengeData[0][i]["prereqresult"].ToString();
            chal.prereqWellbeing = challengeData[0][i]["prereqwellbeing"].ToString(); 
            chal.prereqLanguage = challengeData[0][i]["prereqlanguage"].ToString(); 
            chal.prereqGPA = challengeData[0][i]["prereqgpa"].ToString();
            chal.challengeID = int.Parse(challengeData[0][i]["id"].ToString());
            chal.characterFlag = challengeData[0][i]["characterflag"].ToString();
            chal.locationFlag = int.Parse(challengeData[0][i]["locationflag"].ToString());
            chal.actFlag = int.Parse(challengeData[0][i]["actflag"].ToString());
            chal.title = challengeData[0][i]["title"].ToString();
            chal.flavorText = challengeData[0][i]["flavortxt"].ToString();
            chal.flavorImage = challengeData[0][i]["flavorimg"].ToString();
            chal.optionOneText = challengeData[0][i]["option1txt"].ToString();
            chal.optionTwoText = challengeData[0][i]["option2txt"].ToString();
            chal.optionOneResults = challengeData[0][i]["option1results"].ToString();
            chal.optionTwoResults = challengeData[0][i]["option2results"].ToString();

            challengeDefinitions.Add(chal);
        }
    }

    public void RetrieveChallenge(string chara, int act, string loc) {
        ChallengeDefinition currChallenge = null;
                     
        //use loc, chara, and act to find correct challenge
        switch (chara) {
            case "JohnDoe":
                switch (act) {
                    case 1:
                        switch (loc) {
                            case "Classroom":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_Classroom);
                                break;
                            case "Counselor":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_Counselor);
                                break;
                            case "EdCenter":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_EdCenter);
                                break;
                            case "Cafeteria":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_Cafeteria);
                                break;
                            case "Home":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_Home);
                                break;                            
                        }
                        break;
                    case 2:
                        switch (loc) {
                            case "Phone":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act2_Phone);
                                break;
                            case "TeaHouse":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act2_TeaHouse);
                                break;
                            case "EdCenter":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act2_EdCenter);
                                break;
                            case "Cafeteria":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act2_Cafeteria);
                                break;
                            case "Home":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act2_Home);
                                break;
                        }
                        break;
                    case 3:
                        switch (loc) {
                            case "Classroom":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act3_Classroom);
                                break;
                            case "Counselor":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act3_Counselor);
                                break;
                            case "Stadium":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act3_Stadium);
                                break;
                            case "KaraokeBar":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act3_KaraokeBar);
                                break;
                            case "Home":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act3_Home);
                                break;
                            case "BusStop":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act3_BusStop);
                                break;
                        }
                        break;
                    default:
                        print("not found");
                        break;
                }
                break;
        }
    
        //send challenge definition to the Canvas
        ChallengeCanvas.S.UpdateChallengeCanvas(currChallenge);
    }

    //resolve challenge option
}
