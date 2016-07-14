using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class ParachuteKids : MonoBehaviour {

    /* CLASS VARIABLES
    ---------------------------------------------------------------*/
    static public ParachuteKids S;

    //dictionaries-----------------------//
    static public Dictionary<LocationType, LocationDefinition> LOC_DEFS;
    static public Dictionary<ChallengeType, ChallengeDefinition> CHAL_DEFS;
    static public Dictionary<CharacterType, CharacterDefinition> CHARS_DEFS;
    static public Dictionary<ResultType, ResultDefinition> RES_DEFS;
    static public Dictionary<CollegeType, CollegeDefinition> COL_DEFS;

    //dynamic----------------------------//
    public bool firstRun = true;

    //private----------------------------//
    private JsonData data;

    /* FUNCTIONS
    ---------------------------------------------------------------*/
    void Awake() {
        S = this;

        //init game---------------------------//
        LocationControl.S.GetLocationDefinitions();
        Challenges.S.GetChallengeDefinitions();
        Characters.S.GetCharacterDefinitions();
        EndGame.S.GetCollegeDefinitions();
        Results.S.GetResultDefinitions();
        BuildDictionaries();        

        //init gui and canvas
        GUIControl.S.InitGUI(Player.S.currCharacter);
        ChallengeCanvas.S.FindChallengeCanvasElems();
        GeneralCanvas.S.FindGeneralCanvasElems();
        CollegeCanvas.S.FindCollegeCanvasElems();
    }

    public void StartGame() {        
        Locations.locationObjects = null;
        
        //reset character        
        Player.S.gpa = 2.0f;
        Player.S.wellbeing = 50;
        Player.S.language = 0;
        Player.S.collegeChoice = -1;

        //reset general canvases        
        GeneralCanvas.S.generalCanvas.SetActive(true);
        GeneralCanvas.S.generalGroup.alpha = 1;
        GeneralCanvas.S.generalResultsPanel.SetActive(false);
        GeneralCanvas.S.generalActPanel.SetActive(true);
        GeneralCanvas.S.UpdateActCanvas(1);

        //reset Challenges
        for (int i = 0; i < Challenges.S.totChallenges; i++) {
            ChallengeDefinition chal = GetChallengeDefinition((ChallengeType)i);
            chal.allowedFlag = true;
            chal.clickedFlag = false;
        }

        //reset acts
        Player.S.currAct = 0;
        Acts.S.challengeThisAct = 0;
        Acts.S.challengesDoneForAct = 0;

        //reset other canvases
        ChallengeCanvas.S.challengeCanvas.SetActive(false);
        ChallengeCanvas.S.challengeGroup.alpha = 0;
        CollegeCanvas.S.collegeCanvas.SetActive(false);
        CollegeCanvas.S.collegeGroup.alpha = 0;

        //initialize game
        ArtAssets.S.tileContainer.SetActive(true);
        if (firstRun == true) {
            firstRun = false;
            LocationControl.S.CreateLocations();
        }        
        Acts.S.InitializeAct(Player.S.currAct);
        Invoke("StartCoroutine(GeneralCanvas.S.TransitionActCanvas(0))", 1f);

    }

    public void BuildDictionaries() {

        //build location dictionary---------------//
        LOC_DEFS = new Dictionary<LocationType, LocationDefinition>();
        foreach (LocationDefinition loc in LocationControl.S.locationDefinitions) {
            LOC_DEFS[loc.type] = loc;
        }

        //build challenge dictionary---------------//
        CHAL_DEFS = new Dictionary<ChallengeType, ChallengeDefinition>();
        foreach (ChallengeDefinition chal in Challenges.S.challengeDefinitions) {
            CHAL_DEFS[chal.type] = chal;
        }

        //build characters dictionary---------------//
        CHARS_DEFS = new Dictionary<CharacterType, CharacterDefinition>();
        foreach (CharacterDefinition chara in Characters.S.charactersDefinitions) {
            CHARS_DEFS[chara.type] = chara;
        }

        //build results dictionary---------------//
        RES_DEFS = new Dictionary<ResultType, ResultDefinition>();
        foreach (ResultDefinition result in Results.S.resultDefinitions) {
            RES_DEFS[result.type] = result;
        }

        //build colleges dictionary---------------//
        COL_DEFS = new Dictionary<CollegeType, CollegeDefinition>();
        foreach (CollegeDefinition c in EndGame.S.collegeDefinitions) {
            COL_DEFS[c.type] = c;
        }
    }

    public LocationDefinition GetLocationDefinition(LocationType loc) {
        if (LOC_DEFS.ContainsKey(loc)) {
            return (LOC_DEFS[loc]);
        }
        return (new LocationDefinition());
    }

    public ChallengeDefinition GetChallengeDefinition(ChallengeType ct) {
        if (CHAL_DEFS.ContainsKey(ct)) {
            return(CHAL_DEFS[ct]);
        }
        return (new ChallengeDefinition());
    }

    public CharacterDefinition GetCharacterDefinition(CharacterType chara) {
        if (CHARS_DEFS.ContainsKey(chara)) {
            return (CHARS_DEFS[chara]);
        }
        return (new CharacterDefinition());
    }

    public ResultDefinition GetResultsDefinition(ResultType result) {
        if (RES_DEFS.ContainsKey(result)) {
            return (RES_DEFS[result]);
        }
        return (new ResultDefinition());
    }

    public CollegeDefinition GetCollegeDefinition(CollegeType college) {
        if (COL_DEFS.ContainsKey(college)) {
            return (COL_DEFS[college]);
        }
        return (new CollegeDefinition());
    }

    public void GameOver(int ending) {
        //toggle elems on canvas
        GeneralCanvas.S.resultsFlavor.SetActive(true);
        GeneralCanvas.S.readingResults.SetActive(false);
        GeneralCanvas.S.mathResults.SetActive(false);
        GeneralCanvas.S.writingResults.SetActive(false);
        GeneralCanvas.S.totalResults.SetActive(false);
        GeneralCanvas.S.collegeResults.SetActive(false);

        //modify canvas
        Text flavorText = GeneralCanvas.S.resultsFlavorText.GetComponent<Text>();
        Text titleText = GeneralCanvas.S.resultsTitleText.GetComponent<Text>();
        Text buttonText = GeneralCanvas.S.resultsButtonText.GetComponent<Text>();
        titleText.text = "Game End Screen";
        buttonText.text = "Play again";

        GeneralCanvas.S.resultsButton.onClick.RemoveAllListeners();
        GeneralCanvas.S.resultsButton.onClick.AddListener(delegate { StartGame(); });
    }

  

}
