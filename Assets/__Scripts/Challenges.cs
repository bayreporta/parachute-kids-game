using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public enum ChallengeType {
    JohnDoe_Act1_Classroom
}

public class ChallengeDefinition {
    public ChallengeType type;
    public string characterFlag;
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
    public int totChallenges = 1;
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

            print(chalTypes[i]);
            chal.type = chalTypes[i];
            chal.characterFlag = challengeData[0][i]["characterflag"].ToString();
            chal.actFlag = int.Parse(challengeData[0][i]["actflag"].ToString());
            chal.title = challengeData[0][i]["title"].ToString();
            chal.flavorText = challengeData[0][i]["flavortxt"].ToString();
            chal.FlavorImage = challengeData[0][i]["flavorimg"].ToString();
            chal.OptionOneText = challengeData[0][i]["option1txt"].ToString();
            chal.OptionTwoText = challengeData[0][i]["option2txt"].ToString();

            challengeDefinitions.Add(chal);
        }
    }


}
