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
    JohnDoe_Act1_TeaHouse,
    JohnDoe_Act1_Phone,
    JohnDoe_Act1_Stadium,
    JohnDoe_Act1_KaraokeBar,
    JohnDoe_Act1_BusStop
}

public class ChallengeDefinition {
    public ChallengeType type;
    public bool clickedFlag = false;
    public bool allowedFlag = true;
    public string characterFlag;
    public string locationFlag;
    public int actFlag;
    public string title;
    public string flavorText;
    public string FlavorImage;
    public string OptionOneText;
    public string OptionTwoText;

}
public class Challenges : MonoBehaviour {

    /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    static public Challenges S;
    public int totChallenges = 10;
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
            chal.characterFlag = challengeData[0][i]["characterflag"].ToString();
            chal.locationFlag = challengeData[0][i]["locationflag"].ToString();
            chal.actFlag = int.Parse(challengeData[0][i]["actflag"].ToString());
            chal.title = challengeData[0][i]["title"].ToString();
            chal.flavorText = challengeData[0][i]["flavortxt"].ToString();
            chal.FlavorImage = challengeData[0][i]["flavorimg"].ToString();
            chal.OptionOneText = challengeData[0][i]["option1txt"].ToString();
            chal.OptionTwoText = challengeData[0][i]["option2txt"].ToString();

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
                            case "TeaHouse":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_TeaHouse);
                                break;
                            case "Phone":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_Phone);
                                break;
                            case "Stadium":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_Stadium);
                                break;
                            case "KaraokeBar":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_KaraokeBar);
                                break;
                            case "BusStop":
                                currChallenge = ParachuteKids.S.GetChallengeDefinition(ChallengeType.JohnDoe_Act1_BusStop);
                                break;
                        }
                        break;
                    default:
                        print("not found");
                        break;
                }
                break;
        }
        if (!currChallenge.clickedFlag) { ChallengeCanvas.S.UpdateChallengeCanvas(currChallenge); }
    }


}
