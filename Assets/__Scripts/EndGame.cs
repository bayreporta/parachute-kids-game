using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    public bool admitted = false;
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
    public TextAsset collegeJson;
    public List<CollegeDefinition> collegeDefinitions;
    public List<CollegeType> collegeTypes;

    //Game Over Canvas
    public List<GameObject> ggImages;
    public CanvasGroup ggGroup;
    public Text ggText;
    public Text ggTitle;
    public GameObject ggCanvas;
    public Button playAgain;


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

    public void ConfigureGGCanvas() {
        //configure Game Over
        ggGroup = ggCanvas.GetComponent<CanvasGroup>();
        playAgain.onClick.RemoveAllListeners();
        playAgain.onClick.AddListener(ParachuteKids.S.GameOver);

        EndGame.S.ggGroup.alpha = 0;
        EndGame.S.ggCanvas.SetActive(false);
    }

    public void GetCollegeDefinitions() {
        collegeDefinitions = new List<CollegeDefinition>();
        collegeTypes = new List<CollegeType>();
        collegeData = Utils.S.ConvertJson(collegeJson);

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
        int rand = (int)Math.Round(UnityEngine.Random.Range(minScore, maxScore), 0);
        if (rand > 800) rand = 800;
        readingSATScore = rand;

        rand = (int)Math.Round(UnityEngine.Random.Range(minScore, maxScore), 0);
        if (rand > 800) rand = 800;
        mathSATScore = rand;

        rand = (int)Math.Round(UnityEngine.Random.Range(minScore, maxScore), 0);
        if (rand > 800) rand = 800;
        writingSATScore = rand;
    }

    public bool DetermineCollegeAdmittance() {
        CollegeDefinition college = ParachuteKids.S.GetCollegeDefinition((CollegeType)Player.S.collegeChoice);
        float rand = 0f;
        bool ret = false;

        switch (college.name) {
            case "UC Berkeley":
            case "UC Irvine":

                if (Player.S.gpa >= college.gpaReq) { rand += .7f; } else if (Player.S.gpa >= 3.5) { rand += .2f; }

                if (EndGame.S.readingSATScore >= college.readingReq) rand += .1f;
                if (EndGame.S.mathSATScore >= college.mathReq) rand += .1f;
                if (EndGame.S.writingSATScore >= college.writingReq) rand += .1f;

                //did you get in?
                if (UnityEngine.Random.Range(0f, 1f) <= rand) ret = true;
                break;
            case "CSU Long Beach":
                float score = (Player.S.gpa * 800) + (EndGame.S.readingSATScore + EndGame.S.mathSATScore);

                if (score >= 4000) { rand = 1f; } else if (score >= 3750) { rand = .75f; } else if (score >= 3500) { rand = .5f; } else if (score >= 3200) { rand = .25f; } else { rand = 0f; }

                //did you get in?
                if (UnityEngine.Random.Range(0f, 1f) <= rand) ret = true;
                break;
            case "Pasadena City College":
                ret = true;
                break;
        }
        return ret;
    }

    public void FinalResults() {

        //disable challenge canvas
        ChallengeCanvas.S.challengeCanvas.SetActive(true);
        StartCoroutine(ChallengeCanvas.S.TransitionChallengeCanvas(0));

        //turn on results panel
        GeneralCanvas.S.UpdateSATResultsPanel();

    }

    public IEnumerator TransitionToTheEnd(int i) {
        switch (i) {
            case 0:
                while (ggGroup.alpha > 0) {
                    ggGroup.alpha -= Time.deltaTime * 2;
                    yield return null;
                }        
                ggGroup.interactable = false;
                ggCanvas.SetActive(false);
                break;
            case 1:
                //background                
                ggCanvas.SetActive(true);
                while (ggGroup.alpha < 1) {
                    ggGroup.alpha += Time.deltaTime * 2;
                    yield return null;
                }
                ArtAssets.S.ControlBackground(1);
                GUIControl.S.GUIAlpha.alpha = 0;
                GUIControl.S.GUICanvas.SetActive(false);
                ggGroup.interactable = true;
                break;
        }
    }

    public void PopulateEnding(int ending) {
        //reset gg images in gamespace
        for (int i=0; i < ggImages.Count; i++) { ggImages[i].SetActive(false); }

        //cmon canvas
        GUIControl.S.GUIAlpha.alpha = 0;
        ArtAssets.S.background.SetActive(true);

        switch (ending) {
            case 0:
                ggTitle.text = "HOMEWARD BOUND";
                ggText.text = "The stress of living in a foreign land is too much for you. Living by yourself has been especially difficult coupled with the struggles of grasping English, navigating the sometimes harsh social circles of high school and living up to the expectations of your family. Although your parents are disappointed in your decision to return, you hope that your time in America has prepared you for the future. Maybe you’ll even come back one day and give it another go.";
                ggImages[ending].SetActive(true);
                
                break;
            case 1:
                ggTitle.text = "YOU ARE THE CHAMPION!";
                ggText.text = "You had incredibly high goals for yourself: live alone in a foreign land and overcome the barriers of language, culture and family separation to not graduate high school and be admitted to college. It’s an incredible victory for a young immigrant who knew little English before coming here to get this far, and it’s safe to say that you made your family proud with your achievement!";
                ggImages[ending].SetActive(true);
                break;
            case 2:
                ggTitle.text = "BITTERSWEET VICTORY";
                ggText.text = "Although you didn’t get into the school you wanted, you thankfully have a backup plan to attend community college. With no overbearing requirements to take classes there, you can still continue towards your goal of getting into a top tier college or university after taking classes at Pasadena City College. This setback doesn’t diminish the fact that you overcame incredible obstacles of language, culture and loneliness to start taking classes at an American college.";
                ggImages[ending].SetActive(true);                
                break;
        }

        StartCoroutine(TransitionToTheEnd(1));
    }
}