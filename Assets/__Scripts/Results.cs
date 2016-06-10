﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public enum ResultType {
JohnDoe_Act1_Classroom_Result_AA,
JohnDoe_Act1_Classroom_Result_AB,
JohnDoe_Act1_Counselor_Result_AA,
JohnDoe_Act1_Counselor_Result_AB,
JohnDoe_Act1_Counselor_Result_BA,
JohnDoe_Act1_Counselor_Result_BB,
JohnDoe_Act1_EdCenter_Result_A,
JohnDoe_Act1_EdCenter_Result_B,
JohnDoe_Act1_Cafeteria_Result_A,
JohnDoe_Act1_Cafeteria_Result_BA,
JohnDoe_Act1_Cafeteria_Result_BB,
JohnDoe_Act1_Home_Result_A,
JohnDoe_Act1_Home_Result_B,
JohnDoe_Act2_EdCenter_Result_A,
JohnDoe_Act2_EdCenter_Result_B,
JohnDoe_Act2_Cafeteria_Result_A,
JohnDoe_Act2_Cafeteria_Result_BA,
JohnDoe_Act2_Cafeteria_Result_BB,
JohnDoe_Act2_Home_Result_AA,
JohnDoe_Act2_Home_Result_AB,
JohnDoe_Act2_Home_Result_B,
JohnDoe_Act2_TeaHouse_Result_A,
JohnDoe_Act2_TeaHouse_Result_B,
JohnDoe_Act2_Phone_Result_A,
JohnDoe_Act2_Phone_Result_B,
JohnDoe_Act3_Stadium_Result_A,
JohnDoe_Act3_Stadium_Result_B,
JohnDoe_Act3_Counselor_Result_A,
JohnDoe_Act3_Counselor_Result_BA,
JohnDoe_Act3_Counselor_Result_BB,
JohnDoe_Act3_KaraokeBar_Result_A,
JohnDoe_Act3_KaraokeBar_Result_B,
JohnDoe_Act3_Home_Result_A,
JohnDoe_Act3_Home_Result_B,
JohnDoe_Act3_BusStop_Result_A,
JohnDoe_Act3_BusStop_Result_B,
JohnDoe_Act3_Classroom_Result_A,
JohnDoe_Act3_Classroom_Result_B
}

public class ResultDefinition {
    public ResultType type;
    public int challengeFlag;
    public string resultID;
    public string resultTitle;
    public string resultFlavor;
    public string resultButton;
    public int resultPreqChallenge;
    public string resultPreqResult;
    public float resultSuccessChance;
    public int resultWellbeing;
    public int resultLanguage;
    public float resultGPA;
    public bool resultPicked = false;
}

public class Results : MonoBehaviour {

    /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    public static Results S;
    public int totResults = 38; //manually entered atm
    public JsonData resultData;
    public List<ResultDefinition> resultDefinitions;
    public List<ResultType> resultTypes;


    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void GetResultDefinitions() {
        resultDefinitions = new List<ResultDefinition>();
        resultTypes = new List<ResultType>();
        resultData = Utils.ConvertJson("/_Resources/results.json");

        //grab all values from ChallengeType enum
        foreach (ResultType r in System.Enum.GetValues(typeof(ResultType))) {
            resultTypes.Add(r);
        }

        //build challenge data from json
        for (int i = 0; i < Results.S.totResults; i++) {
            ResultDefinition result = new ResultDefinition();
            result.type = resultTypes[i];
            result.resultID = resultData[0][i]["id"].ToString();
            result.challengeFlag = int.Parse(resultData[0][i]["challengeflag"].ToString());
            result.resultPreqChallenge = int.Parse(resultData[0][i]["prereqchallenge"].ToString());
            result.resultPreqResult = resultData[0][i]["prereqresult"].ToString();
            result.resultTitle = resultData[0][i]["resulttitle"].ToString();
            result.resultFlavor = resultData[0][i]["resulttext"].ToString();
            //result.resultButton = resultData[0][i]["resultbutton"].ToString();
            result.resultSuccessChance = float.Parse(resultData[0][i]["resultschance"].ToString());
            result.resultWellbeing = int.Parse(resultData[0][i]["resultwellbeing"].ToString());
            result.resultLanguage = int.Parse(resultData[0][i]["resultlanguage"].ToString());
            result.resultGPA = float.Parse(resultData[0][i]["resultgpa"].ToString());

            resultDefinitions.Add(result);
        }
    }

    public void RetrieveResult(int chal, string[] opts) {
        ResultDefinition result = new ResultDefinition();
        List<ResultDefinition> results = new List<ResultDefinition>();
        string option = null;
        float rand = -1f;

        //grab results based on challenge key
        for (int i=0; i < Results.S.totResults; i++) {
            ResultDefinition r = ParachuteKids.S.GetResultsDefinition((ResultType)i);
            if (r.challengeFlag == chal) results.Add(r);
        }

        //probability check
        if (opts.Length > 1) {
            rand = Random.Range(0f, 1f);

            //which result wins
            if (opts[0] == "aa") {
                if (rand < results[0].resultSuccessChance) { option = "aa"; }
                else { option = "ab"; }
            }
            else if (opts[0] == "ba") {
                if (results[0].resultID == "a") {
                    if (rand < results[1].resultSuccessChance) { option = "ba"; } 
                    else { option = "bb"; }
                }
                else if (results[0].resultID == "aa") {
                    if (rand < results[2].resultSuccessChance) { option = "ba"; } 
                    else { option = "bb"; }
                }
            }
        } 
        else {option = opts[0];}

        //match option to the correct result
        for (int i=0; i < results.Count; i++) {
            

            //return correct result
            if (results[i].resultID == option) {
                result = results[i];
            }
        }
        ChallengeCanvas.S.UpdateResultCanvas(result);

    }
}
