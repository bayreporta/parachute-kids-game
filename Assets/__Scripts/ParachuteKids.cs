using UnityEngine;
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

        LocationControl.S.CreateLocations();

        //init gui and canvas
        GUI.S.InitGUI(Player.S.currCharacter);
        ChallengeCanvas.S.FindChallengeCanvasElems();
        GeneralCanvas.S.FindGeneralCanvasElems();
        CollegeCanvas.S.FindCollegeCanvasElems();
    }

    void Start() {
        Acts.S.InitializeAct(Player.S.currAct);
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

}
