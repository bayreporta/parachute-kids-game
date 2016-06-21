using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public enum CollegeType {
CSULongBeach,
PasadenaCityCollege,
UCBerkeley,
UCIrvine
}

public class CollegeDefinition {
    public CollegeType type;
    public string name;
    public float gpaReq;
    public int id;
    public int readingReq;
    public int mathReq;
    public int writingReq;
}

public class EndGame : MonoBehaviour {

    /* CLASS VARIABLES
   ---------------------------------------------------------------*/
    public static EndGame S;
    public int totColleges = 4;
    public JsonData collegeData;
    public List<CollegeDefinition> collegeDefinitions;
    public List<CollegeType> collegeTypes;

    //SAT Test
    public int maxSATScoreOverall = 2400;
    public int excellentSATScore = 1800;
    public int averageSATScore = 1500;
    public int poorSATScore = 1260;

    public int maxSATScoreTest = 800;
    public int readingSATScore;
    public int mathSATScore;
    public int writingSATScore;  

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;
    }

    public void GetCollegeDefinitions() {
        collegeDefinitions = new List<CollegeDefinition>();
        collegeTypes = new List<CollegeType>();
        collegeData = Utils.ConvertJson("/_Resources/colleges.json");

        //grab all values from College types enum
        foreach (CollegeType c in System.Enum.GetValues(typeof(CollegeType))) {
            collegeTypes.Add(c);
        }

        //build challenge data from json
        for (int i = 0; i < totColleges; i++) {
            CollegeDefinition college = new CollegeDefinition();

            college.type = collegeTypes[i];
            college.id = int.Parse(collegeData[0][i]["id"].ToString());
            college.name = collegeData[0][i]["college"].ToString();
            college.gpaReq = float.Parse(collegeData[0][i]["gpaavg"].ToString());
            college.readingReq = int.Parse(collegeData[0][i]["satreadingavg"].ToString());
            college.mathReq = int.Parse(collegeData[0][i]["satmathavg"].ToString());
            college.writingReq = int.Parse(collegeData[0][i]["satwritingavg"].ToString());
        
            collegeDefinitions.Add(college);
        }
    }

    public void ProcessSATScores() {
        float minScore = 300 + (Player.S.language * Player.S.gpa);
        float maxScore = 300 + (Player.S.language * (Player.S.gpa * 2));
        ResultDefinition APcheck = ParachuteKids.S.GetResultsDefinition((ResultType)27);

        //Did you take AP classes?
        if (APcheck.resultPicked == true) {
            minScore += 100;
            maxScore += 100;
        }

        //simulate language SAT tests
        int rand = (int)Math.Round(UnityEngine.Random.Range(minScore, maxScore),0);
        if (rand > 800) rand = 800;
        readingSATScore = rand;

        rand = (int)Math.Round(UnityEngine.Random.Range(minScore, maxScore), 0);
        if (rand > 800) rand = 800;
        mathSATScore = rand;

        rand = (int)Math.Round(UnityEngine.Random.Range(minScore, maxScore), 0);
        if (rand > 800) rand = 800;
        writingSATScore = rand;
    }

    public void FinalResults() {
        //disable challenge canvas
        ChallengeCanvas.S.challengeCanvas.SetActive(true);
        StartCoroutine(ChallengeCanvas.S.TransitionChallengeCanvas(0));

        //turn on results panel
        GeneralCanvas.S.UpdateSATResultsPanel();

    }

}
